using Microsoft.EntityFrameworkCore;

using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.Query;

namespace KFC.Gateways.SQLite;

public class AccountRepository : IAccountRepository
{
    private ApplicationContext _context;

    public AccountRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Account account)
    {
        await _context.AddAsync(account);
    }

    public async Task Update(Account account)
    {
        _context.Update(account);
    }

    public async Task<bool> IsUserNameTakenAsync(string username, Account? excludeAccount = null)
    {
        var account = await _context
            .Account
            .FirstOrDefaultAsync(a => a.UserName == username);
        if (account == null) return false;

        if (excludeAccount is not null)
            return excludeAccount.AccountId != account.AccountId;

        return true;
    }
    
    public async Task<Account?> GetAccountByIdAsync(Guid accountId)
    {
        var account = await _context
            .Account
            .FirstOrDefaultAsync(a => a.AccountId == accountId);
        return account;
    }

    public async Task<Account?> GetAccountByUserNameAsync(string userName)
    {
        var account = await _context
            .Account
            .FirstOrDefaultAsync(a => a.UserName == userName);
        return account;
    }

    public async Task<QueryResult<IEnumerable<Account>>> GetAccountAllAsync(int page = 1, int pageSize = 10, QueryFilter<Account>? filter = null)
    {
        var queryable = _context
            .Account
            .AsQueryable();

        if (filter != null) queryable = filter.Apply(queryable);
            
        var totalCount = await queryable.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var results = await queryable.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return QueryResult<IEnumerable<Account>>.Success(results:results, totalCount: totalCount, totalPages: totalPages, pageNumber:page, pageSize: pageSize);

    }

}

