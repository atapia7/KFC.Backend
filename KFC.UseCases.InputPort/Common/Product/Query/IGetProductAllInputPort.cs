using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetProductAllInputPort
{
    Task Handle(GetProductAllDto inputDto);
}
