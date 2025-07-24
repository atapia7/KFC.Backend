using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Enums;


namespace KFC.UseCases.Validator;
public class UpdateChannelValidator: IInputPortValidator<UpdateChannelDto>
{
	private readonly IChannelRepository _repository;
	private readonly IAccountRepository _accountRepository;

	public UpdateChannelValidator(IChannelRepository repository, IAccountRepository accountRepository)
	{
		_repository = repository;
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(UpdateChannelDto inputDto)
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

            if (string.IsNullOrEmpty(inputDto.Name))
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("el Name no puede ser null/vacio") };
                return false;
            }

            var messages = new List<MessageDto>();

            var channel = await _repository.GetChannelByCodeAsync(inputDto.getCode());
            if (channel is not null)
            {
                if (await _repository.IsNameTakenAsync(inputDto.Name, excludeChannel: channel))
                    messages.Add(new MessageDto("Channel ya existe"));
            }

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