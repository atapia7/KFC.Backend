using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetChannelAllInputPort
{
    Task Handle(GetChannelAllDto inputDto);
}
