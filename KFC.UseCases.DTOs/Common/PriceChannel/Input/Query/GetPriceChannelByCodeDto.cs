namespace KFC.UseCases.DTOs.Input;

public class GetPriceChannelByCodeDto: JwtAuthorization
{
    public int PriceChannelCode { get; set; }

}
