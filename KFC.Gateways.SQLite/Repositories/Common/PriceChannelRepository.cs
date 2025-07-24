using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KFC.Gateways.SQLite;

public class PriceChannelRepository : IPriceChannelRepository
{
    private readonly ApplicationContext _context;
    private readonly ILogger<PriceChannelRepository> _logger;

    public PriceChannelRepository(ApplicationContext context, ILogger<PriceChannelRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(PriceChannel PriceChannel)
    {
        _logger.LogInformation("Creando un nuevo PriceChannel: {Id}", PriceChannel.PriceChannelId);
        await _context.AddAsync(PriceChannel);
    }

    public async Task Update(PriceChannel PriceChannel)
    {
        _context.Update(PriceChannel);
    }

    public async Task Delete(PriceChannel PriceChannel)
    {
        _context.Remove(PriceChannel);
    }

    public async Task<PriceChannel?> GetPriceChannelByCodeAsync(int PriceChannelId)
    {
        var PriceChannel = await _context
            .PriceChannel
            .Include(a => a.Channel)
            .Include(a => a.Product)
            .FirstOrDefaultAsync(a => a.PriceChannelId == PriceChannelId);
        return PriceChannel;
    }

    public async Task<QueryResult<IEnumerable<PriceChannel>>> GetPriceChannelAllAsync(int page = 1, int pageSize = 10, QueryFilter<PriceChannel>? filter = null)
    {
        var queryable = _context.PriceChannel
            .Include(a => a.Channel)
            .Include(a => a.Product)
            .AsQueryable();
        if (filter != null) queryable = filter.Apply(queryable);
            
        var totalCount = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var results = await queryable.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return QueryResult<IEnumerable<PriceChannel>>.Success(results:results, totalCount: totalCount, totalPages: totalPages, pageNumber:page, pageSize: pageSize);

    }

   
    public async Task UpdateStatusEnablePriceChannel(PriceChannel PriceChannel)
    {
        if (PriceChannel !=null)
        {
        _context.Update(PriceChannel);
        }
    }

  
}

