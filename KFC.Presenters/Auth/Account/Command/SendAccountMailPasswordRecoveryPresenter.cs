using System.Net;
using KFC.UseCases.DTOs;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public class SendAccountMailPasswordRecoveryPresenter : ISendAccountMailPasswordRecoveryOutputPort
{
    public SendAccountMailPasswordRecoveryPresenter()
    {
        
    }

    public bool IsSuccess { 
        get { return Messages is null; } 
    }  
    public bool? Data { get; set; }
    public IEnumerable<MessageDto?> Messages { get ; set ; }
    public HttpStatusCode HttpStatusCode { get ; set ; }

    public Task HandleFailure(IHandleFailure failure)
    {
        Messages = failure.Messages;
        HttpStatusCode = failure.HttpStatusCode;
        return Task.CompletedTask;
    }

    public Task HandleSuccess(IHandleSuccess<bool> success)
    {
        Data = success.Data;
        HttpStatusCode = success.HttpStatusCode;
        return Task.CompletedTask;
    }
}
