using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Utility;


namespace KFC.UseCases.Validator;

public class GetAccountAllValidator : IInputPortValidator<GetAccountAllDto>
{
	private readonly IAccountRepository _accountRepository;

	public GetAccountAllValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }


    public async Task<bool> IsValid(GetAccountAllDto inputDto)
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
                Messages = new List<MessageDto> { new MessageDto("Usuario no tiene permisos para realizar esta acci�n") };
                return false;
            }

            if (!string.IsNullOrEmpty(inputDto.Status))
            {
                var status = StatusTypeUtility.ConverTo(inputDto.Status);
                if (status is null)
                {
                    HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                    Messages = new List<MessageDto> { new MessageDto("Status no valido (sugerencias: REGISTRADO, ENABLE, DISABLE)") };
                    return false;
                }
            }
            
            if (!string.IsNullOrEmpty(inputDto.AccountType))
            {
                var accountTypeUtility = AccountTypeUtility.ConverTo(inputDto.AccountType);
                if (accountTypeUtility is null)
                {
                    HttpStatusCode = HttpStatusCode.UnprocessableEntity;
                    Messages = new List<MessageDto> { new MessageDto("AccountType no valido (sugerencias: ADMINISTRADOR, CLIENTE, SUPERVISOR, VENDEDOR,SOPORTE)") };
                    return false;
                }
            }

            var messages = new List<MessageDto>();

            messages.AddRange(
                QueryValidator.Validate(
                    queryDto: inputDto
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