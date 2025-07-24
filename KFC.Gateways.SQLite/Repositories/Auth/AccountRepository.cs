using Microsoft.EntityFrameworkCore;

using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using Microsoft.Extensions.Logging;

namespace KFC.Gateways.SQLite;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationContext _context;
    private readonly ILogger<AccountRepository> _logger;

    public AccountRepository(ApplicationContext context, ILogger<AccountRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task CreateAsync(Account account)
    {
        _logger.LogInformation("Creando una nueva cuenta para el usuario: {UserName}", account.UserName);
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
    
    public async Task<Account?> GetAccountByIdAsync(int accountId)
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

