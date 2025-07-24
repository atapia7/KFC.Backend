using KFC.UseCases.DTOs.Output;
using KFC.UseCases.Query;

namespace KFC.UseCases.OutputPort;

public interface IGetSellersByAccountSessionOutputPort : IOutputPort<QueryResult<IEnumerable<AccountDto>>?>
{
    Task HandleSuccess(IHandleSuccess<QueryResult<IEnumerable<AccountDto>>> success);
    Task HandleFailure(IHandleFailure failure);

}