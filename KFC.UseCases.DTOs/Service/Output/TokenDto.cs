namespace KFC.UseCases.DTOs.Output;

public class TokenDto
{
    public TokenDto(string token, DateTime? expiration)
    {
        Token = token;
        Expiration = expiration;
    }

    public string Token { get; set; }
    public DateTime? Expiration { get; set; }

}