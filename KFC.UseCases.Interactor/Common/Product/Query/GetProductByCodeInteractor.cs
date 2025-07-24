using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Service;

namespace KFC.UseCases.Interactor;

public class GetProductByCodeInteractor : IGetProductByCodeInputPort
{
    private readonly IGetProductByCodeOutputPort _outputPort;
    private readonly IProductRepository _repository;
    private readonly IInputPortValidator<GetProductByCodeDto> _validator;

    public GetProductByCodeInteractor(
        IGetProductByCodeOutputPort outputPort, 
        IProductRepository repository,
        IInputPortValidator<GetProductByCodeDto> validator)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetProductByCodeDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Product? Product = await _repository.GetProductByCodeAsync( inputDto.ProductCode);

        ProductDto ProductDto = new ProductDto();
        ProductDto.ProductId = Product.ProductId;
        ProductDto.Name = Product.Name;

        await _outputPort.HandleSuccess(HandleSuccess<ProductDto>.Ok(data: ProductDto));

    }
    
}

