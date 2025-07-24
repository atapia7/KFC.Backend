using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.Entities.Enums;

namespace KFC.UseCases.Interactor;

public class CreateAccountInteractor : ICreateAccountInputPort
{
    private readonly ICreateAccountOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<CreateAccountDto> _validator;

    public CreateAccountInteractor(
        IUnitOfWork unitOfWork,
        IAccountRepository repository,
        ICreateAccountOutputPort outputPort,
        IInputPortValidator<CreateAccountDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(CreateAccountDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Account account = new Account(
            userName : inputDto.UserName,
            email : inputDto.Email,
            accountType: AccountTypeEnum.Customer);

        await _repository.CreateAsync(account);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<int>.Created(data: account.AccountId));

    }

}

