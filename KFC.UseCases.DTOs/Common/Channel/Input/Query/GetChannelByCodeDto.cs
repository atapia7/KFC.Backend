namespace KFC.UseCases.DTOs.Input;

public class GetChannelByCodeDto: JwtAuthorization
{
    /// <summary>
    public int ChannelCode { get; set; }

}
