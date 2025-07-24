namespace KFC.UseCases.OutputPort;

public interface ICreateAccountCustomerOutputPort: IOutputPort<Guid?>
{
    Task HandleSuccess(IHandleSuccess<Guid> success);
    Task HandleFailure(IHandleFailure failure);

}
