using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface ILoginInputPort
{
    Task Handle(LoginDto inputDto);
}
