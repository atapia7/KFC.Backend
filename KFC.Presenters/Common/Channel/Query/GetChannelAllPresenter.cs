using System.Net;
using KFC.UseCases.Query;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;


public class GetChannelAllPresenter : IGetChannelAllOutputPort
{
    public bool IsSuccess { 
        get { return Messages is null; } 
    }  
    public QueryResult<IEnumerable<ChannelDto>>? Data { get; set; }
    public IEnumerable<MessageDto?> Messages { get ; set ; }
    public HttpStatusCode HttpStatusCode { get ; set ; }

    public Task HandleFailure(IHandleFailure failure)
    {
        Messages = failure.Messages;
        HttpStatusCode = failure.HttpStatusCode;
        return Task.CompletedTask;
    }

    public Task HandleSuccess(IHandleSuccess<QueryResult<IEnumerable<ChannelDto>>> success)
    {
        Data = success.Data;
        HttpStatusCode = success.HttpStatusCode;
        return Task.CompletedTask;
    }
}
