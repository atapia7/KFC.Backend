using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Service;

namespace KFC.UseCases.Interactor;

public class GetAccountByUserNameInteractor : IGetAccountByUserNameInputPort
{
    private readonly IGetAccountByUserNameOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IInputPortValidator<GetAccountByUserNameDto> _validator;

    public GetAccountByUserNameInteractor(
        IGetAccountByUserNameOutputPort outputPort, 
        IAccountRepository repository,
        IInputPortValidator<GetAccountByUserNameDto> validator)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetAccountByUserNameDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Account? account = await _repository.GetAccountByUserNameAsync(userName: inputDto.UserName);

        AccountDto accountDto = new AccountDto();
        accountDto.AccountId = account.AccountId;
        accountDto.UserName = account.UserName;
        accountDto.Email = account.Email;
        accountDto.State = StatusTypeUtility.ToString(account.Status);
        accountDto.AccountType = AccountTypeUtility.ToString(account.AccountType);
        

        await _outputPort.HandleSuccess(HandleSuccess<AccountDto>.Ok(data: accountDto));

    }
    
}

