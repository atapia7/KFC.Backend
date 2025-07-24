using KFC.Entities;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interface;

public interface IAccountRepository
{
	Task CreateAsync(Account account);
	Task Update(Account account);

	Task<bool> IsUserNameTakenAsync(string username, Account? excludeAccount = null);
	Task<Account?> GetAccountByIdAsync(Guid accountId);
	Task<Account?> GetAccountByUserNameAsync(string userName);
	
	Task<QueryResult<IEnumerable<Account>>> GetAccountAllAsync(int page = 1, int pageSize = 10, QueryFilter<Account>? filter = null);

}