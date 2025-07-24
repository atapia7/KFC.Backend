using System.Net;

using KFC.Entities.Enums;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities;


namespace KFC.UseCases.Validator;

public class CreateAccountValidator: IInputPortValidator<CreateAccountDto>
{
	private readonly IAccountRepository _accountRepository;
	public CreateAccountValidator(
        IAccountRepository accountRepository
        )
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(CreateAccountDto inputDto)
    {
        try
        {
            var account = await _accountRepository.GetAccountByUserNameAsync(inputDto.getNameClaim());
            if (account is null)
            {
                HttpStatusCode = HttpStatusCode.Forbidden;
                Messages = new List<MessageDto> { new MessageDto("Sesión de usuario no encontrado") };
                return false;
            }

            if (!account.CanCreateAccounts)
            {
                HttpStatusCode = HttpStatusCode.Unauthorized;
                Messages = new List<MessageDto> { new MessageDto("Usuario no tiene permisos para realizar esta acción") };
                return false;
            }

            var messages = new List<MessageDto>();

            if (string.IsNullOrEmpty(inputDto.UserName))
                messages.Add(new MessageDto("Debe ingresar UserName"));

            if (await _accountRepository.IsUserNameTakenAsync(inputDto.UserName))
                messages.Add(new MessageDto("Esta cuenta de usuario ya está tomado"));

            messages.AddRange(
                EmailValidator.Validate(
                    email: inputDto.Email
                )
            );

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