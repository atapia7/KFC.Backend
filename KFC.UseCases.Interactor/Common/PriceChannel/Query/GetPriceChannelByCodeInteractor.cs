using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Service;

namespace KFC.UseCases.Interactor;

public class GetPriceChannelByCodeInteractor : IGetPriceChannelByCodeInputPort
{
    private readonly IGetPriceChannelByCodeOutputPort _outputPort;
    private readonly IPriceChannelRepository _repository;
    private readonly IInputPortValidator<GetPriceChannelByCodeDto> _validator;

    public GetPriceChannelByCodeInteractor(
        IGetPriceChannelByCodeOutputPort outputPort, 
        IPriceChannelRepository repository,
        IInputPortValidator<GetPriceChannelByCodeDto> validator)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetPriceChannelByCodeDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        PriceChannel? PriceChannel = await _repository.GetPriceChannelByCodeAsync(code: inputDto.PriceChannelCode);

        PriceChannelDto PriceChannelDto = new PriceChannelDto(
            priceChannelId: PriceChannel.PriceChannelId,
            productId: PriceChannel.ProductId,
            channelId: PriceChannel.ChannelId,
            amount: PriceChannel.Amount,
            isActive: PriceChannel.IsActive,
            channelName: PriceChannel.Channel?.Name,
            productName: PriceChannel.Product?.Name
            );
   

        await _outputPort.HandleSuccess(HandleSuccess<PriceChannelDto>.Ok(data: PriceChannelDto));

    }
    
}

