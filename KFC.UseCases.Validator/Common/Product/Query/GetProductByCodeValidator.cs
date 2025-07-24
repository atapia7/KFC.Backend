using System.Net;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities;


namespace KFC.UseCases.Validator;
public class GetProductByCodeValidator : IInputPortValidator<GetProductByCodeDto>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IProductRepository _accessoryRepository;


	public GetProductByCodeValidator(IAccountRepository accountRepository, IProductRepository accessoryRepository)
	{
        _accountRepository = accountRepository;
        _accessoryRepository = accessoryRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(GetProductByCodeDto inputDto)
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

            var accessory = await _accessoryRepository.GetProductByCodeAsync(inputDto.ProductCode);
            if ((accessory is null)) 
            {
                HttpStatusCode = HttpStatusCode.NotFound;
                Messages = new List<MessageDto> { new MessageDto("Accessorio no encontrado") };
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