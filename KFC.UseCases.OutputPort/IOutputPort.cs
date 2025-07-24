using KFC.UseCases.DTOs.Interface;

namespace KFC.UseCases.OutputPort;

public interface IOutputPort<T> : IHttpStatus, IMessagesDto
{
	bool IsSuccess { get; }
    public T? Data { get; set; }
}