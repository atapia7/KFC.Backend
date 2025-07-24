using KFC.Entities;
using KFC.Entities.Enums;
using KFC.Entities.Utility;
using KFC.UseCases.Query;

namespace KFC.UseCases.Interactor;

public class AccountAllFilter : QueryFilter<Account>
{
  
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Status { get; set; }
    public string? AccountType { get; set; }
    
    public override IQueryable<Account> Apply(IQueryable<Account> queryable)
    {
        if (UserName is not null) 
        {
            queryable = queryable.Where(p=> p.UserName.ToLower().Contains(UserName));
        }        
        if (Email is not null) queryable = queryable.Where(p => p.Email.Equals(Email));
        if (Status is not null) queryable = queryable.Where(p => p.Status == StatusTypeUtility.ConverTo(Status));
        if (AccountType is not null) queryable = queryable.Where(p => p.AccountType == AccountTypeUtility.ConverTo(AccountType));

       

        return queryable;
    }
}
    