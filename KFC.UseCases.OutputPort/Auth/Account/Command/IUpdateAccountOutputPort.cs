namespace KFC.UseCases.OutputPort;

public interface IUpdateAccountOutputPort: IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);

}
