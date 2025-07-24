using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;


namespace KFC.UseCases.Validator;

public class GetAccountByUserNameAndTokenValidator : IInputPortValidator<GetAccountByUserNameAndTokenDto>
{
	private readonly IAccountRepository _accountRepository;

	public GetAccountByUserNameAndTokenValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(GetAccountByUserNameAndTokenDto inputDto)
    {
        try
        {
            if (string.IsNullOrEmpty(inputDto.UserName))
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { new MessageDto("UserName no puede ser null") };
                return false;
            }
            
            if (string.IsNullOrEmpty(inputDto.Token))
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { new MessageDto("Token no puede ser null") };
                return false;
            }

            var account = await _accountRepository.GetAccountByUserNameAsync(inputDto.UserName);
            if (account is null)
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { new MessageDto("Usuario no encontrado") };
                return false;
            }

            if (account.Token != inputDto.Token)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto(code: "0001", description: "Token erroneo") };
                return false;
            }

            if (account.ExpireToken < DateTime.Now)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto(code: "0001", description: "Token expirado") };
                return false;
            }

            var messages = new List<MessageDto>();

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