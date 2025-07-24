using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities;

namespace KFC.UseCases.Interactor;

public class GetChannelAllInteractor : IGetChannelAllInputPort
{
    private readonly IGetChannelAllOutputPort _outputPort;
    private readonly IChannelRepository _repository;
    private readonly IInputPortValidator<GetChannelAllDto> _validator;

    public GetChannelAllInteractor(
        IGetChannelAllOutputPort outputPort, 
        IChannelRepository repository,
        IInputPortValidator<GetChannelAllDto> validator
   )
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetChannelAllDto inputDto)
    {

        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        ChannelAllFilter queryFilterDto = new ChannelAllFilter();
        queryFilterDto.Name = string.IsNullOrEmpty(inputDto.FilterByNameLike) ? null :inputDto.FilterByNameLike.ToLower();

        var Channels = await _repository.GetChannelAllAsync(page: inputDto.Page, pageSize: inputDto.PageSize, filter: queryFilterDto);

        List<ChannelDto> ChannelsDto = new List<ChannelDto>();
        foreach (var Channel in Channels.Results)
        {
            ChannelDto ChannelDto = new ChannelDto();
            ChannelDto.ChannelId = Channel.ChannelId;
            ChannelDto.Name = Channel.Name;
            ChannelsDto.Add(ChannelDto);
        }
        
        var resultDto = QueryResult<IEnumerable<ChannelDto>>.Success(
            results:ChannelsDto, 
            totalCount: Channels.TotalCount, 
            totalPages: Channels.TotalPages, 
            pageNumber: inputDto.Page, 
            pageSize: inputDto.PageSize
        );

        await _outputPort.HandleSuccess(HandleSuccess<QueryResult<IEnumerable<ChannelDto>>>.Ok(data: resultDto));

    }
    
}

