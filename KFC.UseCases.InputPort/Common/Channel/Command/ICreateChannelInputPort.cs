using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface ICreateChannelInputPort
{
    Task Handle(CreateChannelDto inputDto);
}
