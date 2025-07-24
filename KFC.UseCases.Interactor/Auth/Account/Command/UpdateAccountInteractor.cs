using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.Entities.Utility;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class UpdateAccountInteractor : IUpdateAccountInputPort
{
    private readonly IUpdateAccountOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<UpdateAccountDto> _validator;

    public UpdateAccountInteractor(
        IUnitOfWork unitOfWork,
        IAccountRepository repository,
        IUpdateAccountOutputPort outputPort,
        IInputPortValidator<UpdateAccountDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(UpdateAccountDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }
        
        Account? account = await _repository.GetAccountByUserNameAsync(userName: inputDto.getUserName());

        account!.Email = inputDto.Email;
        
        await _repository.Update(account);
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

