using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class UpdatePriceChannelInteractor : IUpdatePriceChannelInputPort
{
    private readonly IUpdatePriceChannelOutputPort _outputPort;
    private readonly IPriceChannelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<UpdatePriceChannelDto> _validator;

    public UpdatePriceChannelInteractor(
        IUnitOfWork unitOfWork,
        IPriceChannelRepository repository,
        IUpdatePriceChannelOutputPort outputPort,
        IInputPortValidator<UpdatePriceChannelDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(UpdatePriceChannelDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        PriceChannel? PriceChannel = await _repository.GetPriceChannelByCodeAsync(inputDto.getCode());

        PriceChannel!.Amount = inputDto.Amount;
        
        await _repository.Update(PriceChannel);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

