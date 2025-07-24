using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities;


namespace KFC.UseCases.Validator;

public class UpdatePasswordAccountValidator: IInputPortValidator<UpdatePasswordAccountDto>
{
	private readonly IAccountRepository _accountRepository;

	public UpdatePasswordAccountValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(UpdatePasswordAccountDto inputDto)
    {
        try
        {
            var account = await _accountRepository.GetAccountByUserNameAsync(inputDto.getUserName());
            if (account is null)
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { new MessageDto("Usuario no valido") };
                return false;
            }

            if (account.Token != inputDto.Token)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("Token erroneo") };
                return false;
            }

            if (account.ExpireToken < DateTime.Now)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("Token expirado") };
                return false;
            }

            var messages = new List<MessageDto>();

            if (string.IsNullOrEmpty(inputDto.Password))
            {
                messages.Add(ErrorUtility.CreateFromCode(errorCode: "0003"));
            }
            else
            {
                messages.AddRange(
                    PasswordValidator.Validate(
                    password: inputDto.Password
                ));
            }


            if (string.IsNullOrEmpty(inputDto.Password2))
            {
                messages.Add(ErrorUtility.CreateFromCode(errorCode: "0004"));
            }
            else
            {
                messages.AddRange(
                    PasswordValidator.Validate(
                    password: inputDto.Password2
                ));
            }

            if (inputDto.Password2 != inputDto.Password)
                messages.Add(ErrorUtility.CreateFromCode(errorCode: "0005"));


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