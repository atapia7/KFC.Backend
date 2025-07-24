using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface ICreatePriceChannelInputPort
{
    Task Handle(CreatePriceChannelDto inputDto);
}
