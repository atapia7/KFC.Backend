using KFC.UseCases.DTOs.Output;

namespace KFC.UseCases.OutputPort;

public interface IGetAccountByUserNameOutputPort : IOutputPort<AccountDto?>
{
    Task HandleSuccess(IHandleSuccess<AccountDto> success);
    Task HandleFailure(IHandleFailure failure);

}
