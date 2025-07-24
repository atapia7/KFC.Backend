namespace KFC.UseCases.DTOs.Input;

public class DeletePriceChannelDto: JwtAuthorization
{
    /// <summary>
    /// Identificadodor del codigo de accesorio
    /// </summary>           
    public int PriceChannelCode { get; set; }

}
