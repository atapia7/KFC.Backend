using System.Net;

using KFC.UseCases.DTOs;

namespace KFC.UseCases.OutputPort;

public interface IHandleFailure
{
    public IEnumerable<MessageDto> Messages { get; }
    public HttpStatusCode HttpStatusCode { get; }
}