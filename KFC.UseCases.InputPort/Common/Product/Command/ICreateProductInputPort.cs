using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface ICreateProductInputPort
{
    Task Handle(CreateProductDto inputDto);
}
