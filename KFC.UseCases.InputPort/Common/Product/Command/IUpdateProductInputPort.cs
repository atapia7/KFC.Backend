using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface IUpdateProductInputPort
{
    Task Handle(UpdateProductDto inputDto);
}
