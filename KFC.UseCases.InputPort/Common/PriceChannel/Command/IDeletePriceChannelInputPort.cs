using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface IDeletePriceChannelInputPort
{
    Task Handle(DeletePriceChannelDto inputDto);
}
