using KFC.UseCases.DTOs.Output;

namespace KFC.UseCases.OutputPort;

public interface IGetAccountByUserNameAndTokenOutputPort : IOutputPort<AccountByUserNameAndTokenDto?>
{
    Task HandleSuccess(IHandleSuccess<AccountByUserNameAndTokenDto> success);
    Task HandleFailure(IHandleFailure failure);

}
