using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface IUpdateChannelInputPort
{
    Task Handle(UpdateChannelDto inputDto);
}
