using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface IDeleteProductInputPort
{
    Task Handle(DeleteProductDto inputDto);
}
