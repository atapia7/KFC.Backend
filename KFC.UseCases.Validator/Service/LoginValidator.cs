using System.Net;
using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Validator;

public class LoginValidator: IInputPortValidator<LoginDto>	
{
	private readonly IAccountRepository _accountRepository;

	public LoginValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(LoginDto inputDto)
    {
        try
        {
            var messages = new List<MessageDto>();

            if (string.IsNullOrEmpty(inputDto.UserName))
                messages.Add(new MessageDto("Debe ingresar la cuenta"));

            messages.AddRange(
                PasswordValidator.Validate(
                   password: inputDto.Password
                )
            );

            Account? account = await _accountRepository.GetAccountByUserNameAsync(inputDto.UserName);
            if (account is null)
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { new MessageDto("Usuario no encontrado") };
                return false;
            }

            if (account.Status != StatusEnum.Enabled)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("Usuario no esta activo") };
                return false;
            }

            Messages = messages;
            HttpStatusCode = messages.Count == 0 ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            return messages.Count == 0;
        }
        catch (Exception ex)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Messages = new List<MessageDto> { new MessageDto(String.Format("Ocurrio un error en el momento de crear la cuenta: {0}", ex.Message)) };
            return false;
        }
    }

}