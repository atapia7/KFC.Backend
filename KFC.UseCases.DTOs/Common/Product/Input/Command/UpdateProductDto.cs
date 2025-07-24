namespace KFC.UseCases.DTOs.Input;

public class UpdateProductDto : JwtAuthorization
{
    /// <summary>Nuevo nombre del producto (opcional).</summary>
    public string? Name { get; set; }

    /// <summary>Nueva descripción del producto (opcional).</summary>
    public string? Description { get; set; }

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
