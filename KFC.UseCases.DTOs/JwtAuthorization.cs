namespace KFC.UseCases.DTOs.Input;

public class JwtAuthorization
{
    private string m_nameClaim;

    public void setNameClaim(string nameClaim)
    {
        m_nameClaim = nameClaim;
    }
    public string getNameClaim()
    {
        return m_nameClaim;
    }
}