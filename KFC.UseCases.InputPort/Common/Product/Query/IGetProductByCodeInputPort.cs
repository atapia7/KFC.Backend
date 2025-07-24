using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetProductByCodeInputPort
{
    Task Handle(GetProductByCodeDto inputDto);
}
