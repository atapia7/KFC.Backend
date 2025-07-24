using KFC.Entities;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interface;

public interface IChannelRepository
{
	Task CreateAsync(Channel Channel);
	Task Update(Channel Channel);

	Task<Channel?> GetChannelByCodeAsync(int code);
	Task<Channel?> GetChannelByNameAsync(string name);
     Task<bool> IsNameTakenAsync(string name, Channel? excludeChannel=null);
    Task<QueryResult<IEnumerable<Channel>>> GetChannelAllAsync(int page = 1, int pageSize = 10, QueryFilter<Channel>? filter = null);

}