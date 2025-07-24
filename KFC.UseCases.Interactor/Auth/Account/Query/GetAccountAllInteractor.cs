using KFC.UseCases.Interface;
using KFC.UseCases.Query;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.DTOs.Output;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities;
using KFC.UseCases.Service;
using KFC.Entities.Utility;

namespace KFC.UseCases.Interactor;

public class GetAccountAllInteractor : IGetAccountAllInputPort
{
    private readonly IGetAccountAllOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IInputPortValidator<GetAccountAllDto> _validator;

    public GetAccountAllInteractor(
        IGetAccountAllOutputPort outputPort, 
        IAccountRepository repository,
        IInputPortValidator<GetAccountAllDto> validator
   )
    {
        _outputPort = outputPort;
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(GetAccountAllDto inputDto)
    {

        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        AccountAllFilter queryFilterDto = new AccountAllFilter();
        queryFilterDto.UserName = string.IsNullOrEmpty(inputDto.UserName) ? null :inputDto.UserName.ToLower();
        queryFilterDto.Email = inputDto.Email;
        queryFilterDto.Status = inputDto.Status;
        queryFilterDto.AccountType = inputDto.AccountType;

        var accounts = await _repository.GetAccountAllAsync(page: inputDto.Page, pageSize: inputDto.PageSize, filter: queryFilterDto);

        List<AccountDto> accountsDto = new List<AccountDto>();
        foreach (var account in accounts.Results)
        {
            AccountDto accountDto = new AccountDto();
            accountDto.AccountId = account.AccountId;
            accountDto.UserName = account.UserName;
            accountDto.Email = account.Email;
            accountDto.State = StatusTypeUtility.ToString(statustype: account.Status);
            accountDto.AccountType = AccountTypeUtility.ToString(account.AccountType);
            accountsDto.Add(accountDto);
        }
        
        var resultDto = QueryResult<IEnumerable<AccountDto>>.Success(
            results:accountsDto, 
            totalCount: accounts.TotalCount, 
            totalPages: accounts.TotalPages, 
            pageNumber: inputDto.Page, 
            pageSize: inputDto.PageSize
        );

        await _outputPort.HandleSuccess(HandleSuccess<QueryResult<IEnumerable<AccountDto>>>.Ok(data: resultDto));

    }
    
}

