namespace KFC.UseCases.DTOs.Input;

public class UpdateAccountDto: JwtAuthorization
{

    public string Email { get; set; }

    private string m_userName;
    public void setUserName(string userName) {
        m_userName = userName;
    }
    public string getUserName()
    {
        return m_userName;
    }
}
