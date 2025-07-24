using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities;

namespace KFC.UseCases.Interactor;

public class GetPriceChannelAllInteractor : IGetPriceChannelAllInputPort
{
    private readonly IGetPriceChannelAllOutputPort _outputPort;
    private readonly IPriceChannelRepository _repository;
    private readonly IInputPortValidator<GetPriceChannelAllDto> _validator;

    public GetPriceChannelAllInteractor(
        IGetPriceChannelAllOutputPort outputPort, 
        IPriceChannelRepository repository,
        IInputPortValidator<GetPriceChannelAllDto> validator
   )
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetPriceChannelAllDto inputDto)
    {

        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        PriceChannelAllFilter queryFilterDto = new PriceChannelAllFilter();
        queryFilterDto.isActive = inputDto.FilterByIsActive;

        var PriceChannels = await _repository.GetPriceChannelAllAsync(page: inputDto.Page, pageSize: inputDto.PageSize, filter: queryFilterDto);

        List<PriceChannelDto> PriceChannelsDto = new List<PriceChannelDto>();
        foreach (var PriceChannel in PriceChannels.Results)
        {
            PriceChannelDto PriceChannelDto = new PriceChannelDto(
            priceChannelId: PriceChannel.PriceChannelId,
            productId: PriceChannel.ProductId,
            channelId: PriceChannel.ChannelId,
            amount: PriceChannel.Amount,
            isActive: PriceChannel.IsActive,
            channelName: PriceChannel!.Channel!?.Name!,
            productName: PriceChannel!.Product!?.Name!
            );
            PriceChannelsDto.Add(PriceChannelDto);
        }
        
        var resultDto = QueryResult<IEnumerable<PriceChannelDto>>.Success(
            results:PriceChannelsDto, 
            totalCount: PriceChannels.TotalCount, 
            totalPages: PriceChannels.TotalPages, 
            pageNumber: inputDto.Page, 
            pageSize: inputDto.PageSize
        );

        await _outputPort.HandleSuccess(HandleSuccess<QueryResult<IEnumerable<PriceChannelDto>>>.Ok(data: resultDto));

    }
    
}

