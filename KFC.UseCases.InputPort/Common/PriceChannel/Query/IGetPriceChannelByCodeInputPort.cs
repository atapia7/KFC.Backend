using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetPriceChannelByCodeInputPort
{
    Task Handle(GetPriceChannelByCodeDto inputDto);
}
