using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IAccountActivateInputPort
{
    Task Handle(AccountActivateDto inputDto);
}
