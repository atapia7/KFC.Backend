using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Enums;


namespace KFC.UseCases.Validator;

public class AccountActivateValidator : IInputPortValidator<AccountActivateDto>
{
	private readonly IAccountRepository _accountRepository;

	public AccountActivateValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(AccountActivateDto inputDto)
    {
        try
        {
            var account = await _accountRepository.GetAccountByUserNameAsync(inputDto.getUserName());
            if (account is null)
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { ErrorUtility.CreateFromCode(errorCode: "0001") };
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

            if (account.Status != StatusEnum.Registered)
            {
                HttpStatusCode = HttpStatusCode. BadRequest;
                Messages = new List<MessageDto> { ErrorUtility.CreateFromCode(errorCode: "00011") };
                return false;
            }

            var messages = new List<MessageDto>();


            if (string.IsNullOrEmpty(inputDto.Password)) {
                messages.Add(ErrorUtility.CreateFromCode(errorCode: "0003"));
            }
            else { 
                messages.AddRange(
                    PasswordValidator.Validate(
                    password: inputDto.Password
                ));
            }


            if (string.IsNullOrEmpty(inputDto.Password2)) {
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
            Messages = new List<MessageDto> { ErrorUtility.CreateFromCode(errorCode: "0000") };
            return false;
        }
    }
}