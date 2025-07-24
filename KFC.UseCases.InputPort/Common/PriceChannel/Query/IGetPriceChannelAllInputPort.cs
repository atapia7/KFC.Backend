using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetPriceChannelAllInputPort
{
    Task Handle(GetPriceChannelAllDto inputDto);
}
