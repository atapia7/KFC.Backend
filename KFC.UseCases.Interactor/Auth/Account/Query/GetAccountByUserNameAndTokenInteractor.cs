using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class GetAccountByUserNameAndTokenAndTokenInteractor : IGetAccountByUserNameAndTokenInputPort
{
    private readonly IGetAccountByUserNameAndTokenOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IInputPortValidator<GetAccountByUserNameAndTokenDto> _validator;

    public GetAccountByUserNameAndTokenAndTokenInteractor(
        IGetAccountByUserNameAndTokenOutputPort outputPort, 
        IAccountRepository repository,
        IInputPortValidator<GetAccountByUserNameAndTokenDto> validator)
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetAccountByUserNameAndTokenDto inputDto)
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

        AccountByUserNameAndTokenDto accountDto = new AccountByUserNameAndTokenDto();
        accountDto.UserName =account.UserName;
        

        await _outputPort.HandleSuccess(HandleSuccess<AccountByUserNameAndTokenDto>.Ok(data: accountDto));

    }
    
}

