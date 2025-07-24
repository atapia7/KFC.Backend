namespace KFC.UseCases.DTOs.Input;


public class GetProductAllDto: GetQueryDto
{
    public string? FilterByName { get; set; }

}
