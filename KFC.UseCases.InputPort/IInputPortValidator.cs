using KFC.UseCases.DTOs.Interface;

namespace KFC.UseCases.InputPort;

public interface IInputPortValidator<T> : IHttpStatus, IMessagesDto
{
    Task<bool> IsValid(T entityDto);
}
