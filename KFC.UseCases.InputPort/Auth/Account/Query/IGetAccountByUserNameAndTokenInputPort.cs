using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetAccountByUserNameAndTokenInputPort
{
    Task Handle(GetAccountByUserNameAndTokenDto inputDto);
}
