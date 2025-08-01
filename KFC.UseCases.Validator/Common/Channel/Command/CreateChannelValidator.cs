using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Utility;
using KFC.Entities.Enums;


namespace KFC.UseCases.Validator;

public class CreateChannelValidator: IInputPortValidator<CreateChannelDto>
{
	private readonly IAccountRepository _accountRepository;
	private readonly IChannelRepository _repository;

	public CreateChannelValidator(IChannelRepository repository, IAccountRepository accountRepository)
	{
		_repository = repository;
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(CreateChannelDto inputDto)
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

            if (inputDto.Name.Length>120)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("limite de caracteres superados en Nombre") };
                return false;
            }

            if (string.IsNullOrEmpty(inputDto.Name))
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("el nombre del accessorio no puede ser null/vacio") };
                return false;
            }
           

            var messages = new List<MessageDto>();

            if (await _repository.IsNameTakenAsync(inputDto.Name))
                messages.Add(new MessageDto("CHannel ya existe"));

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