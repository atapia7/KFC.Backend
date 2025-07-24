using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Enums;


namespace KFC.UseCases.Validator;
public class UpdatePriceChannelValidator: IInputPortValidator<UpdatePriceChannelDto>
{
	private readonly IPriceChannelRepository _repository;
	private readonly IAccountRepository _accountRepository;

	public UpdatePriceChannelValidator(IPriceChannelRepository repository, IAccountRepository accountRepository)
	{
		_repository = repository;
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(UpdatePriceChannelDto inputDto)
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
            if (!account.CanCrud)
            {
                HttpStatusCode = HttpStatusCode.Unauthorized;
                Messages = new List<MessageDto> { new MessageDto("Usuario no tiene permisos para realizar esta acción") };
                return false;
            }

            if (inputDto.Amount<0)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("Amount no valido") };
                return false;
            }

            var messages = new List<MessageDto>();

            var channel = await _repository.GetPriceChannelByCodeAsync(inputDto.getCode());
            if (channel is  null)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("el PriceChannel no existe") };
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Messages = new List<MessageDto> { new MessageDto(String.Format("Ocurrio un error en el momento de crear la cuenta: {0}", ex.Message)) };
            return false;
        }
    }

}