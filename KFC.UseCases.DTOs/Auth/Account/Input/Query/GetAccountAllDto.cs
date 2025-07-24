namespace KFC.UseCases.DTOs.Input;

public class GetAccountAllDto: GetQueryDto
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Status { get; set; }
    public string AccountType { get; set; }
}
