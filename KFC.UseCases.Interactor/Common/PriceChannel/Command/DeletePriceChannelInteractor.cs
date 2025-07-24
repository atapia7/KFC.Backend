using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class DeletePriceChannelInteractor : IDeletePriceChannelInputPort
{
    private readonly IDeletePriceChannelOutputPort _outputPort;
    private readonly IPriceChannelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<DeletePriceChannelDto> _validator;

    public DeletePriceChannelInteractor(
        IUnitOfWork unitOfWork,
        IPriceChannelRepository repository,
        IDeletePriceChannelOutputPort outputPort,
        IInputPortValidator<DeletePriceChannelDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(DeletePriceChannelDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        PriceChannel? PriceChannel = await _repository.GetPriceChannelByCodeAsync(inputDto.PriceChannelCode);

        PriceChannel!.Status = StatusEnum.Disabled;
        
        await _repository.Update(PriceChannel);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

