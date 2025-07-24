namespace KFC.UseCases.DTOs.Input;

public class EnableAccountDto: JwtAuthorization
{
    /// <summary>
    /// Identificadodor del account
    /// </summary>           
    public string UserName { get; set; }

}
