namespace KFC.UseCases.DTOs.Input;

public class DeleteProductDto: JwtAuthorization
{
    /// <summary>
    /// Identificadodor del codigo Product
    /// </summary>           
    public int ProductCode { get; set; }

}
