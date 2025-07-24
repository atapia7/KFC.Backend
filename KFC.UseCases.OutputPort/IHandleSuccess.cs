using System.Net;

namespace KFC.UseCases.OutputPort;

public interface IHandleSuccess<T>
{
	public T? Data { get; }
    HttpStatusCode HttpStatusCode { get; }
}