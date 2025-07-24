using KFC.UseCases.DTOs.Output;

namespace KFC.UseCases.OutputPort;

public interface IGetAccountSessionOutputPort: IOutputPort<AccountSessionDto?>
{
    Task HandleSuccess(IHandleSuccess<AccountSessionDto> success);
    Task HandleFailure(IHandleFailure failure);

}
