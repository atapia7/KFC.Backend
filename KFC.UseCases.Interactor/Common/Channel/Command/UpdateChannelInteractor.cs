using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class UpdateChannelInteractor : IUpdateChannelInputPort
{
    private readonly IUpdateChannelOutputPort _outputPort;
    private readonly IChannelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<UpdateChannelDto> _validator;

    public UpdateChannelInteractor(
        IUnitOfWork unitOfWork,
        IChannelRepository repository,
        IUpdateChannelOutputPort outputPort,
        IInputPortValidator<UpdateChannelDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(UpdateChannelDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        Channel? Channel = await _repository.GetChannelByCodeAsync(inputDto.getCode());

        Channel!.Name = inputDto.Name;
        
        await _repository.Update(Channel);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

