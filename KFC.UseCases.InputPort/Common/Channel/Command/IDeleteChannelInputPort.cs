using KFC.UseCases.DTOs.Input;


namespace KFC.UseCases.InputPort;

public interface IDeleteChannelInputPort
{
    Task Handle(DeleteChannelDto inputDto);
}
