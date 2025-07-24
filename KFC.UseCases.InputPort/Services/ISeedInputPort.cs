using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface ISeedInputPort
{
    Task Handle(SeedDto inputDto);
    
}
