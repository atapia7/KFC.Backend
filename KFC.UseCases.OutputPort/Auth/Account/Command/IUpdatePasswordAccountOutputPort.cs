namespace KFC.UseCases.OutputPort;

public interface IUpdatePasswordAccountOutputPort: IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);

}
