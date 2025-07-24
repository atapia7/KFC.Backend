namespace KFC.UseCases.DTOs.Input;

public class GetAccountByUserNameDto: JwtAuthorization
{
    /// <summary>
    /// Nombre de la numeracion
    /// </summary>           
    public string UserName { get; set; }

}
