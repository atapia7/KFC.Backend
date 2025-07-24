using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IEnableAccountInputPort
{
    Task Handle(EnableAccountDto inputDto);
}
