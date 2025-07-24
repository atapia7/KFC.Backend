using KFC.Entities;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interface;

public interface IPriceChannelRepository
{
	Task CreateAsync(PriceChannel PriceChannel);
	Task Update(PriceChannel PriceChannel);

	Task<PriceChannel?> GetPriceChannelByCodeAsync(int code);
	
	Task<QueryResult<IEnumerable<PriceChannel>>> GetPriceChannelAllAsync(int page = 1, int pageSize = 10, QueryFilter<PriceChannel>? filter = null);

}