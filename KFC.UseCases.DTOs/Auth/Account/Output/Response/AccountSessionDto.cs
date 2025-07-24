namespace KFC.UseCases.DTOs.Output;

public  class AccountSessionDto
{
    public Guid AccountId { get; set; }

    public string UserName { get; set; }
    public string Email { get; set; }
    public string AccountType { get; set; }
    public string State { get; set; }

}
