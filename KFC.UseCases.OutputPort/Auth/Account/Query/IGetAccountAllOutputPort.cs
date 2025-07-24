using KFC.UseCases.Query;
using KFC.UseCases.DTOs.Output;


namespace KFC.UseCases.OutputPort;

public interface IGetAccountAllOutputPort: IOutputPort<QueryResult<IEnumerable<AccountDto>>?>
{
    Task HandleSuccess(IHandleSuccess<QueryResult<IEnumerable<AccountDto>>> success);
    Task HandleFailure(IHandleFailure failure);

}
