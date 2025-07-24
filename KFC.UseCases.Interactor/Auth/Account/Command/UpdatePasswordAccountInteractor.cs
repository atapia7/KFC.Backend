using KFC.Entities;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.Interface;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Utility;

namespace KFC.UseCases.Interactor;

public class UpdatePasswordAccountInteractor : IUpdatePasswordAccountInputPort
{
    private readonly IUpdatePasswordAccountOutputPort _outputPort;
    private readonly IAccountRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInputPortValidator<UpdatePasswordAccountDto> _validator;

    public UpdatePasswordAccountInteractor(
        IUnitOfWork unitOfWork,
        IAccountRepository repository,
        IUpdatePasswordAccountOutputPort outputPort,
        IInputPortValidator<UpdatePasswordAccountDto> validator 
        )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _outputPort = outputPort;
        _validator = validator;
    }

    public async Task Handle(UpdatePasswordAccountDto inputDto)
    {
        bool IsValid = await _validator.IsValid(inputDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        Account? account = await _repository.GetAccountByUserNameAsync(inputDto.getUserName());
        account!.setPassword(password: PasswordHasher.HashPassword(inputDto.Password));
        account.setToken(account.Token!, DateTime.Now.ToUniversalTime());
        await _repository.Update(account);

        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<bool>.Ok(data: true));

    }

}

