namespace KFC.UseCases.DTOs.Input;


public class GetPriceChannelAllDto: GetQueryDto
{
    public bool? FilterByIsActive { get; set; }

}
