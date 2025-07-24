namespace KFC.UseCases.DTOs.Input;

public class GetAccountByIdDto: JwtAuthorization
{
    /// <summary>
    /// Nombre de la numeracion
    /// </summary>           
    public Guid AccountId { get; set; }

}
