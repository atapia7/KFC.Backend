namespace KFC.UseCases.OutputPort;

public interface ISendMailAccountActivateOutputPort: IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);

}
