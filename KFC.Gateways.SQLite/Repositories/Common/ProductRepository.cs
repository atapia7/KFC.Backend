using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KFC.Gateways.SQLite;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ApplicationContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(Product Product)
    {
        _logger.LogInformation("Creando un nuevo producto: {Name}", Product.Name);
        await _context.AddAsync(Product);
    }

    public async Task Update(Product Product)
    {
        _context.Update(Product);
    }

    public async Task Delete(Product Product)
    {
        _context.Remove(Product);
    }

    public async Task<Product?> GetProductByCodeAsync(int ProductId)
    {
        var Product = await _context
            .Product
            .FirstOrDefaultAsync(a => a.ProductId == ProductId);
        return Product;
    }

    public async Task<QueryResult<IEnumerable<Product>>> GetProductAllAsync(int page = 1, int pageSize = 10, QueryFilter<Product>? filter = null)
    {
        var queryable = _context.Product.AsQueryable();
        if (filter != null) queryable = filter.Apply(queryable);
            
        var totalCount = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var results = await queryable.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return QueryResult<IEnumerable<Product>>.Success(results:results, totalCount: totalCount, totalPages: totalPages, pageNumber:page, pageSize: pageSize);

    }

    public async Task<Product?> GetProductByNameAsync(string name)
    {
        var Product = await _context
            .Product
            .FirstOrDefaultAsync(a => a.Name == name);
        return Product;
    }

    public async Task<bool> IsNameTakenAsync(string name, Product? excludeProduct = null)
    {
        var Product = await _context.Product.FirstOrDefaultAsync(a => a.Name == name);
        if (Product == null) return false;

        if (excludeProduct is not null)
            return excludeProduct.ProductId != Product.ProductId;

        return true;
    }

    public async Task UpdateStatusEnableProduct(Product Product)
    {
        if (Product !=null)
        {
        _context.Update(Product);
        }
    }
}

