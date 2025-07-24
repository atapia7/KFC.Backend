namespace KFC.UseCases.DTOs.Input;

public class UpdatePriceChannelDto : JwtAuthorization
{

    /// <summary>
    /// Nuevo monto del precio (opcional).
    /// </summary>
    public decimal Amount { get; set; }

    /// <summary>
    /// Indica si el precio permanece activo (opcional).
    /// </summary>
    public bool? IsActive { get; set; }

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
