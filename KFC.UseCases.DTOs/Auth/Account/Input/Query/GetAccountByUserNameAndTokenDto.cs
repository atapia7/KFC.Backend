namespace KFC.UseCases.DTOs.Input;

public class GetAccountByUserNameAndTokenDto: JwtAuthorization
{
    /// <summary>
    /// Nombre de la cuenta
    /// </summary>           
    public string UserName { get; set; }

    /// <summary>
    /// Token de la cuenta
    /// </summary>
    public string Token { get; set; }

}
