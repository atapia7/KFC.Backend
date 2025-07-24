using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Service;

namespace KFC.UseCases.Interactor;

public class GetChannelByCodeInteractor : IGetChannelByCodeInputPort
{
    private readonly IGetChannelByCodeOutputPort _outputPort;
    private readonly IChannelRepository _repository;
    private readonly IInputPortValidator<GetChannelByCodeDto> _validator;

    public GetChannelByCodeInteractor(
        IGetChannelByCodeOutputPort outputPort, 
        IChannelRepository repository,
        IInputPortValidator<GetChannelByCodeDto> validator)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetChannelByCodeDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Channel? Channel = await _repository.GetChannelByCodeAsync(code: inputDto.ChannelCode);

        ChannelDto ChannelDto = new ChannelDto();
        ChannelDto.ChannelId = Channel.ChannelId;
        ChannelDto.Name = Channel.Name;

        await _outputPort.HandleSuccess(HandleSuccess<ChannelDto>.Ok(data: ChannelDto));

    }
    
}

