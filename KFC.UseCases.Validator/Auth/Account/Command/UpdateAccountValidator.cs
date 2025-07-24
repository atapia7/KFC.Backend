using System.Net;
using KFC.Entities.Enums;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;


namespace KFC.UseCases.Validator;

public class UpdateAccountValidator: IInputPortValidator<UpdateAccountDto>
{
	private readonly IAccountRepository _accountRepository;

	public UpdateAccountValidator(
        IAccountRepository accountRepository
        )
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(UpdateAccountDto inputDto)
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

            var accountEdit = await _accountRepository.GetAccountByUserNameAsync(inputDto.getUserName());
            if (accountEdit is null)
            {
                HttpStatusCode = HttpStatusCode.Forbidden;
                Messages = new List<MessageDto> { new MessageDto("Usuario no encontrado") };
                return false;
            }

            AccountTypeEnum accounType = accountEdit.AccountType;
            var messages = new List<MessageDto>();

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