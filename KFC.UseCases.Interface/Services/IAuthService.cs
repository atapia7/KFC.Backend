namespace KFC.UseCases.Service;

public interface IAuthService
{
	Task<string> GenerateToken(string userName);
	Task<bool> ValidateToken(string token);
}