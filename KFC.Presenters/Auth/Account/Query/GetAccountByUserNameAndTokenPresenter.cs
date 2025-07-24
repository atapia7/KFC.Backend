using System.Net;
using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public class GetAccountByUserNameAndTokenPresenter : IGetAccountByUserNameAndTokenOutputPort
{

    public bool IsSuccess { 
        get { return Messages is null; } 
    }  
    public AccountByUserNameAndTokenDto? Data { get; set; }
    public IEnumerable<MessageDto?> Messages { get ; set ; }
    public HttpStatusCode HttpStatusCode { get ; set ; }

    public Task HandleFailure(IHandleFailure failure)
    {
        Messages = failure.Messages;
        HttpStatusCode = failure.HttpStatusCode;
        return Task.CompletedTask;
    }

    public Task HandleSuccess(IHandleSuccess<AccountByUserNameAndTokenDto> success)
    {
        Data = success.Data;
        HttpStatusCode = success.HttpStatusCode;
        return Task.CompletedTask;
    }
}
