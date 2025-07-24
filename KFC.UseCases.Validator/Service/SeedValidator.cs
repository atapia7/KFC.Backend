using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;

namespace KFC.UseCases.Validator;

public class SeedValidator : IInputPortValidator<SeedDto>
{
	private readonly IAccountRepository _repository;

	public SeedValidator(IAccountRepository repository)
	{
		_repository = repository;
	}

    public IEnumerable<MessageDto> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(SeedDto entityDto)
    {
        try
        {
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