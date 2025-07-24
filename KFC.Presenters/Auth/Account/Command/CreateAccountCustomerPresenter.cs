using System.Net;
using KFC.UseCases.DTOs;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public class CreateAccountCustomerPresenter : ICreateAccountCustomerOutputPort
{

    public bool IsSuccess { 
        get { return Messages is null; } 
    }  
    public Guid? Data { get; set; }
    public IEnumerable<MessageDto?> Messages { get ; set ; }
    public HttpStatusCode HttpStatusCode { get ; set ; }

    public Task HandleFailure(IHandleFailure failure)
    {
        Messages = failure.Messages;
        HttpStatusCode = failure.HttpStatusCode;
        return Task.CompletedTask;
    }

    public Task HandleSuccess(IHandleSuccess<Guid> success)
    {
        Data = success.Data;
        HttpStatusCode = success.HttpStatusCode;
        return Task.CompletedTask;
    }
}
