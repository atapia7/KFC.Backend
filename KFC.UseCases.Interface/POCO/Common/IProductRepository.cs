using KFC.Entities;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interface;

public interface IProductRepository
{
	Task CreateAsync(Product Product);
	Task Update(Product Product);

	Task<Product?> GetProductByCodeAsync(int ProductId);
	Task<Product?> GetProductByNameAsync(string Name);
    Task<bool> IsNameTakenAsync(string name, Product? excludeProduct = null);
    Task<QueryResult<IEnumerable<Product>>> GetProductAllAsync(int page = 1, int pageSize = 10, QueryFilter<Product>? filter = null);

}