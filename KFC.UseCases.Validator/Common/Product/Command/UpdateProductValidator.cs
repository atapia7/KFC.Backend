using System.Net;

using KFC.UseCases.Interface;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.Entities.Enums;


namespace KFC.UseCases.Validator;

public class UpdateProductValidator: IInputPortValidator<UpdateProductDto>
{
	private readonly IProductRepository _repository;
	private readonly IAccountRepository _accountRepository;

	public UpdateProductValidator(IProductRepository repository, IAccountRepository accountRepository)
	{
		_repository = repository;
        _accountRepository = accountRepository;
	}

    public IEnumerable<MessageDto?> Messages { get; set; }
    public HttpStatusCode HttpStatusCode { get; set; }

    public async Task<bool> IsValid(UpdateProductDto inputDto)
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

            var product = await _repository.GetProductByCodeAsync(inputDto.getCode());
            if (product is null)
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("el producto no existe") };
                return false;
            }

            if (string.IsNullOrEmpty(inputDto.Name))
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("el nombre del producto no puede ser null/vacio") };
                return false;
            }
            
            if (string.IsNullOrEmpty(inputDto.Description))
            {
                HttpStatusCode = HttpStatusCode.BadRequest;
                Messages = new List<MessageDto> { new MessageDto("la descripcion de producto no puede ser null/vacio") };
                return false;
            }

            var messages = new List<MessageDto>();

            if (product is not null)
            {
                if (await _repository.IsNameTakenAsync(name: inputDto.Name, excludeProduct: product))
                    messages.Add(new MessageDto($"Producto {product.Name} ya existe"));
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