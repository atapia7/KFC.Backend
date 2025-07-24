using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IDisableAccountInputPort
{
    Task Handle(DisableAccountDto inputDto);
}
