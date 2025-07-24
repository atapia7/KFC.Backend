using KFC.UseCases.DTOs.Output;

namespace KFC.UseCases.OutputPort;

public interface IGetAccountByIdOutputPort: IOutputPort<AccountDto?>
{
    Task HandleSuccess(IHandleSuccess<AccountDto> success);
    Task HandleFailure(IHandleFailure failure);

}
