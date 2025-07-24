namespace KFC.UseCases.OutputPort;

public interface ICreateAccountOutputPort: IOutputPort<int?>
{
    Task HandleSuccess(IHandleSuccess<int> success);
    Task HandleFailure(IHandleFailure failure);

}
