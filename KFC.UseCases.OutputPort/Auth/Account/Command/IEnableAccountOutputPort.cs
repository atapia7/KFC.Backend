namespace KFC.UseCases.OutputPort;

public interface IEnableAccountOutputPort: IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);
}
