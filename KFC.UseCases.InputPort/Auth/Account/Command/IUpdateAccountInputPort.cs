using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IUpdateAccountInputPort
{
    Task Handle(UpdateAccountDto inputDto);
}
