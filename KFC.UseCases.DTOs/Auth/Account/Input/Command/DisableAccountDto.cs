namespace KFC.UseCases.DTOs.Input;

public class DisableAccountDto: JwtAuthorization
{
    /// <summary>
    /// Identificadodor del proudcto
    /// </summary>           
    public string UserName { get; set; }

}
