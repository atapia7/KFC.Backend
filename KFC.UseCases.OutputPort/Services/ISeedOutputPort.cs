namespace KFC.UseCases.OutputPort;

public interface ISeedOutputPort: IOutputPort<string?>
{
    Task HandleSuccess(IHandleSuccess<string> success);
    Task HandleFailure(IHandleFailure failure);

}
