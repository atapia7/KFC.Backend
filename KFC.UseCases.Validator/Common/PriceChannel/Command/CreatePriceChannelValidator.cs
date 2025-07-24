using System.Net;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities;
using KFC.UseCases.Interface;

namespace KFC.UseCases.Validator;

public class CreatePriceChannelValidator : IInputPortValidator<CreatePriceChannelDto>
{
    private readonly IPriceChannelRepository _repository;
    private readonly IAccountRepository _accountRepository;

    public CreatePriceChannelValidator(
        IPriceChannelRepository repository,
        IAccountRepository accountRepository)
    {
        _repository = repository;
        _accountRepository = accountRepository;
    }

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(CreatePriceChannelDto inputDto)
    {
        try
        {
            var account = await _accountRepository.GetAccountByUserNameAsync(inputDto.getNameClaim());
            if (account is null)
            {
                HttpStatusCode = HttpStatusCode.Forbidden;
                Messages = new List<MessageDto> { new MessageDto("Sesión de usuario no encontrada") };
                return false;
            }

            if (!account.CanCrud)
            {
                HttpStatusCode = HttpStatusCode.Unauthorized;
                Messages = new List<MessageDto> { new MessageDto("Usuario no tiene permisos para realizar esta acción") };
                return false;
            }

            var messages = new List<MessageDto>();

            var existsActive = await _repository.ExistsActiveAsync(inputDto.ProductId,inputDto.ChannelId);
            if (existsActive)
            {
                messages.Add(new MessageDto(
                    $"Ya existe un precio activo para el producto {inputDto.ProductId} en el canal {inputDto.ChannelId}"
                ));
            }

            if (inputDto.Amount <= 0)
            {
                messages.Add(new MessageDto("El monto debe ser mayor que cero"));
            }

            Messages = messages;
            HttpStatusCode = messages.Any() ? HttpStatusCode.BadRequest : HttpStatusCode.OK;

            return !messages.Any();
        }
        catch (Exception ex)
        {
            HttpStatusCode = HttpStatusCode.InternalServerError;
            Messages = new List<MessageDto>
            {
                new MessageDto($"Ocurrió un error al validar el precio: {ex.Message}")
            };
            return false;
        }
    }
}
