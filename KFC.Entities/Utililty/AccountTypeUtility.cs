using KFC.Entities.Enums;

namespace KFC.Entities.Utility;

public class AccountTypeUtility
{
    private const string ADMINISTRATOR = "ADMINISTRADOR";
    private const string CUSTOMER = "CLIENTE";
    private const string SUPERVISOR = "SUPERVISOR";
    private const string SELLER = "VENDEDOR";
    private const string SUPPORT = "SOPORTE";

    public static string ToString(AccountTypeEnum accountType)
    {
        if (accountType == AccountTypeEnum.Administrator) return ADMINISTRATOR;
        if (accountType == AccountTypeEnum.Customer) return CUSTOMER;
        if (accountType == AccountTypeEnum.Supervisor) return SUPERVISOR;
        if (accountType == AccountTypeEnum.Seller) return SELLER;
        if (accountType == AccountTypeEnum.Support) return SUPPORT;


        return "";
    }

    public static AccountTypeEnum? ConverTo(string accountType)
    {
        if (string.IsNullOrEmpty(accountType)) return null;

        if (accountType.ToUpper() == ADMINISTRATOR) return AccountTypeEnum.Administrator;
        else if (accountType.ToUpper() == CUSTOMER) return AccountTypeEnum.Customer;
        else if (accountType.ToUpper() == SUPERVISOR) return AccountTypeEnum.Supervisor;
        else if (accountType.ToUpper() == SELLER) return AccountTypeEnum.Seller;
        else if (accountType.ToUpper() == SUPPORT) return AccountTypeEnum.Support;
        return null;
    }

    public static bool IsAdministrator(string accountType)
    {
        return accountType.ToUpper() == ADMINISTRATOR;
    } 

    public static bool IsCustomer(string accountType)
    {
        return accountType.ToUpper() == CUSTOMER;
    }
    public static bool IsSupervisor(string accountType)
    {
        return accountType.ToUpper() == SUPERVISOR;
    }
    public static bool IsSeller(string accountType)
    {
        return accountType.ToUpper() == SELLER;
    }

}
