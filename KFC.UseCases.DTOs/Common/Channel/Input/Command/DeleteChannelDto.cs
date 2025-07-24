namespace KFC.UseCases.DTOs.Input;

public class DeleteChannelDto: JwtAuthorization
{
    /// <summary>
    /// Identificadodor del codigo de accesorio
    /// </summary>           
    public int ChannelCode { get; set; }

}
