namespace KFC.UseCases.DTOs.Input;

public class GetProductByCodeDto: JwtAuthorization
{
    public int ProductCode { get; set; }

}
