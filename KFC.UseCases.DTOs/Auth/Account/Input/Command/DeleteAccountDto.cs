namespace KFC.UseCases.DTOs.Input;

public class DeleteAccountDto: JwtAuthorization
{
    /// <summary>
    /// Identificadodor del Accountt
    /// </summary>           
    public string UserName { get; set; }

}
