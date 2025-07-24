using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;

namespace KFC.UseCases.Interactor;

public class DisableAccountInteractor : IDisableAccountInputPort
{
    private readonly IDisableAccountOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<DisableAccountDto> _validator;

    public DisableAccountInteractor(
        IUnitOfWork unitOfWork,
        IAccountRepository repository,
        IDisableAccountOutputPort outputPort,
        IInputPortValidator<DisableAccountDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(DisableAccountDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Account? account = await _repository.GetAccountByUserNameAsync(inputDto.UserName);
        account.setDisable();

        await _repository.Update(account);

        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

