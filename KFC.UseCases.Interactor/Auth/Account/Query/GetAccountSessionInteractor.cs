using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Service;

namespace KFC.UseCases.Interactor;

public class GetAccountSessionInteractor : IGetAccountSessionInputPort
{
    private readonly IGetAccountSessionOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IInputPortValidator<GetSessionDto> _validator;

    public GetAccountSessionInteractor(
        IGetAccountSessionOutputPort outputPort,
        IAccountRepository repository,
        IInputPortValidator<GetSessionDto> validator
        )
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetSessionDto inputDto)
    {

        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        Account? account = await _repository.GetAccountByUserNameAsync(userName: inputDto.getNameClaim());

        AccountSessionDto accountDto = new AccountSessionDto();
        accountDto.UserName = account.UserName;
        accountDto.Email = account.Email;
        accountDto.AccountType = AccountTypeUtility.ToString(account.AccountType);
        accountDto.State = StatusTypeUtility.ToString(account.Status);

        await _outputPort.HandleSuccess(HandleSuccess<AccountSessionDto>.Ok(data: accountDto));

    }
}

