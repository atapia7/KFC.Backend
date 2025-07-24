using KFC.UseCases.DTOs;
using KFC.UseCases.DTOs.Input;

namespace KFC.UseCases.InputPort;

public interface IGetAccountByUserNameInputPort
{
    Task Handle(GetAccountByUserNameDto inputDto);
}
