using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KFC.Gateways.SQLite;

public class ChannelRepository : IChannelRepository
{
    private readonly ApplicationContext _context;
    private readonly ILogger<ChannelRepository> _logger;

    public ChannelRepository(ApplicationContext context, ILogger<ChannelRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(Channel Channel)
    {
        _logger.LogInformation("Creando un nuevo canal: {Name}", Channel.Name);
        await _context.AddAsync(Channel);
    }

    public async Task Update(Channel Channel)
    {
        _context.Update(Channel);
    }

    public async Task Delete(Channel Channel)
    {
        _context.Remove(Channel);
    }

    public async Task<Channel?> GetChannelByCodeAsync(int ChannelId)
    {
        var Channel = await _context
            .Channel
            .FirstOrDefaultAsync(a => a.ChannelId == ChannelId);
        return Channel;
    }

    public async Task<QueryResult<IEnumerable<Channel>>> GetChannelAllAsync(int page = 1, int pageSize = 10, QueryFilter<Channel>? filter = null)
    {
        var queryable = _context.Channel.AsQueryable();
        if (filter != null) queryable = filter.Apply(queryable);
            
        var totalCount = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var results = await queryable.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return QueryResult<IEnumerable<Channel>>.Success(results:results, totalCount: totalCount, totalPages: totalPages, pageNumber:page, pageSize: pageSize);

    }

    public async Task<Channel?> GetChannelByNameAsync(string name)
    {
        var Channel = await _context
            .Channel
            .FirstOrDefaultAsync(a => a.Name == name);
        return Channel;
    }

    public async Task<bool> IsNameTakenAsync(string name, Channel? excludeChannel = null)
    {
        var Channel = await _context.Channel.FirstOrDefaultAsync(a => a.Name == name);
        if (Channel == null) return false;

        if (excludeChannel is not null)
            return excludeChannel.ChannelId != Channel.ChannelId;

        return true;
    }

    public async Task UpdateStatusEnableChannel(Channel Channel)
    {
        if (Channel !=null)
        {
        _context.Update(Channel);
        }
    }
}

