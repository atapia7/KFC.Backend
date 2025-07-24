namespace KFC.UseCases.OutputPort;

public interface ICreateAccountDealerOutputPort: IOutputPort<Guid?>
{
    Task HandleSuccess(IHandleSuccess<Guid> success);
    Task HandleFailure(IHandleFailure failure);

}
