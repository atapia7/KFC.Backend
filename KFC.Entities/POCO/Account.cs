using KFC.Entities.Enums;

namespace KFC.Entities;

/// <summary>
/// Representa un usuario del sistema.
/// </summary>
public class Account : Base
{
    public Account(int accountId, string userName, string email, AccountTypeEnum accountType)
    {
        AccountId = accountId;
        UserName = userName;
        Email = email;
        AccountType = accountType;
    }
    public Account( string userName, string email, AccountTypeEnum accountType)
    {
        UserName = userName;
        Email = email;
        AccountType = accountType;
    }
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public int AccountId { get; set; }

    /// <summary>
    /// Nombre de usuario único para autenticación.
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// Hash de la contraseña para autenticación segura.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    /// Tipo de cuenta
    /// </summary>           
    public AccountTypeEnum AccountType { get; set; }

    /// <summary>
    /// Token de creacion o cambio de contrasena
    /// </summary>  
    public string? Token { get; internal set; }

    /// <summary>
    /// Caducidad de token
    /// </summary>  
    public DateTime? ExpireToken { get; internal set; }

    /// <summary>
    /// Productos creados o gestionados por este usuario (opcional).
    /// </summary>
    public ICollection<Product> Products { get; set; } = new List<Product>();






    public bool CanCreateAccounts
    {
        get
        {
            bool can = AccountType == AccountTypeEnum.Administrator;
            can = can || AccountType == AccountTypeEnum.Supervisor;
            return can;
        }
    }
    public bool CanCrud
    {
        get
        {
            bool can = AccountType == AccountTypeEnum.Administrator;
            return can;
        }
    }

    public void setPassword(string password)
    {
        this.PasswordHash = password;
    }
    
    public Account(string? token, DateTime? expireToken)
    {
        Token = token;
        ExpireToken = expireToken;
    }

    public void setToken(string token, DateTime expired)
    {
        Token = token;
        ExpireToken = expired;
    }

}


