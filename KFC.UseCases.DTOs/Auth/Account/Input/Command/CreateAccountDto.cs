namespace KFC.UseCases.DTOs.Input;

public class CreateAccountDto: JwtAuthorization
{
    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }


}
