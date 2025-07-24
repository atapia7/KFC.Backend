using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface IUpdatePriceChannelInputPort
{
    Task Handle(UpdatePriceChannelDto inputDto);
}
