using KFC.UseCases.DTOs.Output;

namespace KFC.UseCases.OutputPort;

public interface ILoginOutputPort: IOutputPort<TokenDto?>
{
    Task HandleSuccess(IHandleSuccess<TokenDto> success);
    Task HandleFailure(IHandleFailure failure);


}
