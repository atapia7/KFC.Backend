using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Enums;


namespace KFC.UseCases.Validator;

public class DisableAccountValidator: IInputPortValidator<DisableAccountDto>
{
	private readonly IAccountRepository _accountRepository;

	public DisableAccountValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(DisableAccountDto inputDto)
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
            if (string.IsNullOrEmpty(inputDto.UserName))
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto($"El username es requerido") };
                return false;
            }
            
            var accountDisable = await _accountRepository.GetAccountByUserNameAsync(inputDto.UserName);

            if (accountDisable is null)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("Usuario no encontrado") };
                return false;
            }


       

            
            var messages = new List<MessageDto>();

            if (accountDisable.Status != StatusEnum.Enabled)
                messages.Add(new MessageDto("No se puede desactivar un usuario que no este activo"));

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