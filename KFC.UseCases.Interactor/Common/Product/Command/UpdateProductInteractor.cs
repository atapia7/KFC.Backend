using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class UpdateProductInteractor : IUpdateProductInputPort
{
    private readonly IUpdateProductOutputPort _outputPort;
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<UpdateProductDto> _validator;

    public UpdateProductInteractor(
        IUnitOfWork unitOfWork,
        IProductRepository repository,
        IUpdateProductOutputPort outputPort,
        IInputPortValidator<UpdateProductDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(UpdateProductDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        Product? Product = await _repository.GetProductByCodeAsync(inputDto.getCode());

        Product!.Name = inputDto.Name;
        
        await _repository.Update(Product);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

