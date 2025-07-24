using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Utility;

namespace KFC.UseCases.Interactor;

public class LoginInteractor : ILoginInputPort
{
    private readonly ILoginOutputPort _outputPort;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<LoginDto> _validator;

    public LoginInteractor(
		IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        ILoginOutputPort outputPort,
        IInputPortValidator<LoginDto> validator
    )
	{
		_unitOfWork = unitOfWork;
        _accountRepository = accountRepository;
        _outputPort = outputPort;
        _validator = validator;
    }
	
	public async Task Handle(LoginDto entityDto)
	{

        bool IsValid = await _validator.IsValid(entityDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }


        Account? account = await _accountRepository.GetAccountByUserNameAsync(userName: entityDto.UserName);
        if (account is not null)
        {
            if (PasswordHasher.VerifyPassword(entityDto.Password, account.PasswordHash))
            {
                DateTime expires = DateTime.UtcNow.AddHours(8);
                string token = GenerateJwtToken(username: account.UserName, expires: expires);
                TokenDto tokenDto = new TokenDto(token: token, expiration: expires);
                await _outputPort.HandleSuccess(HandleSuccess<TokenDto>.Ok(tokenDto));
            }
            else
            {
                await _outputPort.HandleFailure(HandleFailure.BadRequest(
                        messages: new List<MessageDto> { new MessageDto("Clave incorrecta") }
                    ));
            }

        }
        else
        {
            await _outputPort.HandleFailure(HandleFailure.NotFound(
                    messages: new List<MessageDto> { new MessageDto("Cuenta no existe") }
                ));
        }
    }
	
	private string GenerateJwtToken(string username, DateTime expires)
	{
		string secretKey = "aapKey"; 
		byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
		byte[] adjustedKeyBytes = new byte[32]; // 128 bits = 16 bytes
		Array.Copy(keyBytes, adjustedKeyBytes, Math.Min(keyBytes.Length, adjustedKeyBytes.Length));

		var securityKey = new SymmetricSecurityKey(adjustedKeyBytes);
		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: "https://yourdomain.com",
			audience: "https://api.yourdomain.com",
			claims: new[] { new Claim(ClaimTypes.Name, username) },
			expires: expires,
			signingCredentials: credentials
		);

		var tokenHandler = new JwtSecurityTokenHandler();
		return tokenHandler.WriteToken(token);
	}	
	

}