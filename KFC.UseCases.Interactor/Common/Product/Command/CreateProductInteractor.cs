using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class CreateProductInteractor : ICreateProductInputPort
{
    private readonly ICreateProductOutputPort _outputPort;
    private readonly IProductRepository _repository;
    private readonly IAccountRepository _accountRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<CreateProductDto> _validator;

    public CreateProductInteractor(
        IUnitOfWork unitOfWork,
        IProductRepository repository,
        IAccountRepository accountRepository,
        ICreateProductOutputPort outputPort,
        IInputPortValidator<CreateProductDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _accountRepository = accountRepository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(CreateProductDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        var user = await  _accountRepository.GetAccountByUserNameAsync(inputDto.getNameClaim());

        Product Product = new Product(
            name: inputDto.Name,
            description: inputDto.Description,
            owner: user);

        await _repository.CreateAsync(Product);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<string>.Created(data: Product.ProductId.ToString()));

    }

}

