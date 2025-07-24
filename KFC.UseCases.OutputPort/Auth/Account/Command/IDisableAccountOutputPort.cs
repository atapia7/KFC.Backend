namespace KFC.UseCases.OutputPort;

public interface IDisableAccountOutputPort: IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);
}
