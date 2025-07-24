namespace KFC.UseCases.DTOs.Input;

public class GetQueryDto : JwtAuthorization
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}