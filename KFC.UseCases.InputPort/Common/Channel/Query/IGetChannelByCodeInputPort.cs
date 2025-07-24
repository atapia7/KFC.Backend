using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetChannelByCodeInputPort
{
    Task Handle(GetChannelByCodeDto inputDto);
}
