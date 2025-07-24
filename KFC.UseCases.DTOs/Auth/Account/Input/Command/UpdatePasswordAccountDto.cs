namespace KFC.UseCases.DTOs.Input;

public class UpdatePasswordAccountDto : JwtAuthorization
{
    /// <summary>
    /// Identificadodor de la cuenta
    /// </summary>           
    public string Password { get; set; }
    public string Password2 { get; set; }
    public string Token { get; set; }

    private string m_userName;

    public void setUserName(string userName)
    {
        m_userName = userName;
    }
    public string getUserName()
    {
        return m_userName;
    }
}
