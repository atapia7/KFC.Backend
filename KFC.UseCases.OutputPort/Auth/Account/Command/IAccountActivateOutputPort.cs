namespace KFC.UseCases.OutputPort;

public interface IAccountActivateOutputPort : IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);

}
