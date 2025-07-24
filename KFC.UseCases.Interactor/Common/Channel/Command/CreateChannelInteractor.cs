using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class CreateChannelInteractor : ICreateChannelInputPort
{
    private readonly ICreateChannelOutputPort _outputPort;
    private readonly IChannelRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<CreateChannelDto> _validator;

    public CreateChannelInteractor(
        IUnitOfWork unitOfWork,
        IChannelRepository repository,
        ICreateChannelOutputPort outputPort,
        IInputPortValidator<CreateChannelDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(CreateChannelDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Channel Channel = new Channel(name: inputDto.Name);

        await _repository.CreateAsync(Channel);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<string>.Created(data: Channel.ChannelId.ToString()));

    }

}

