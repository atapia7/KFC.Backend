using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class DeleteProductInteractor : IDeleteProductInputPort
{
    private readonly IDeleteProductOutputPort _outputPort;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<DeleteProductDto> _validator;

    public DeleteProductInteractor(
        IUnitOfWork unitOfWork,
        IProductRepository repository,
        IDeleteProductOutputPort outputPort,
        IInputPortValidator<DeleteProductDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(DeleteProductDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        Product? Product = await _repository.GetProductByCodeAsync(inputDto.ProductCode);

        Product!.Status = StatusEnum.Disabled;
        
        await _repository.Update(Product);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

