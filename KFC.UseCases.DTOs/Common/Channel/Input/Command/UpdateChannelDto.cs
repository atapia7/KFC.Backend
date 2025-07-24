namespace KFC.UseCases.DTOs.Input;

public class UpdateChannelDto: JwtAuthorization
{
    /// <summary>
    /// Descripcion breve del nombre
    /// </summary>           
    public string Name { get; set; }
    
    /// <summary>
    /// codigo enviado desde la url
    /// </summary>       
    private int m_code;


    public void setCode(int code)
    {
        m_code = code;
    }    
    public int getCode()
    {
        return m_code;
    }      
}
