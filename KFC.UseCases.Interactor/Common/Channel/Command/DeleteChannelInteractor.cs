using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class DeleteChannelInteractor : IDeleteChannelInputPort
{
    private readonly IDeleteChannelOutputPort _outputPort;
    private readonly IChannelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<DeleteChannelDto> _validator;

    public DeleteChannelInteractor(
        IUnitOfWork unitOfWork,
        IChannelRepository repository,
        IDeleteChannelOutputPort outputPort,
        IInputPortValidator<DeleteChannelDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(DeleteChannelDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        Channel? Channel = await _repository.GetChannelByCodeAsync(inputDto.ChannelCode);

        Channel!.Status = StatusEnum.Disabled;
        
        await _repository.Update(Channel);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

