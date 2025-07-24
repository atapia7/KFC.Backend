namespace KFC.UseCases.OutputPort;

public interface ISendAccountMailPasswordRecoveryOutputPort: IOutputPort<bool?>
{
    Task HandleSuccess(IHandleSuccess<bool> success);
    Task HandleFailure(IHandleFailure failure);

}
