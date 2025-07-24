using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;


namespace KFC.UseCases.Validator;

public class GetProductAllValidator : IInputPortValidator<GetProductAllDto>
{
	private readonly IAccountRepository _accountRepository;

	public GetProductAllValidator(IAccountRepository accountRepository)
	{
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(GetProductAllDto inputDto)
    {
        try
        {
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