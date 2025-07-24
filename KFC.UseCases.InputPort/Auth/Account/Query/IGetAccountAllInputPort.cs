using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetAccountAllInputPort
{
    Task Handle(GetAccountAllDto inputDto);
}
