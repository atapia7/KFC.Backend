using System.Net;

using KFC.UseCases.DTOs;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class HandleFailure : IHandleFailure
{
	public IEnumerable<MessageDto> Messages { get;}
	public HttpStatusCode HttpStatusCode { get; set; }

	private HandleFailure(IEnumerable<MessageDto> messages, HttpStatusCode statusCode)
	{
        Messages = messages;
		HttpStatusCode = statusCode;

    }
	
	public static HandleFailure NotFound(IEnumerable<MessageDto> messages)
	{
		return new HandleFailure(messages: messages, statusCode: HttpStatusCode.NotFound);
	}

    public static HandleFailure BadRequest(IEnumerable<MessageDto> messages)
    {
        return new HandleFailure(messages: messages, statusCode: HttpStatusCode.BadRequest);
    }

    public static HandleFailure Fail(IEnumerable<MessageDto> messages, HttpStatusCode statusCode)
    {
        return new HandleFailure(messages: messages, statusCode: statusCode);
    }

}