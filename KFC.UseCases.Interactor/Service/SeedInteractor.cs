using KFC.Entities;
using KFC.UseCases.Interface;
using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using KFC.UseCases.OutputPort;
using KFC.UseCases.Utility;
using KFC.Entities.Enums;
using Microsoft.Extensions.Logging;

namespace KFC.UseCases.Interactor;

public class SeedInteractor :  ISeedInputPort
{
    private readonly ISeedOutputPort _outputPort;
    private readonly IAccountRepository _accountRepository;
    private readonly IInputPortValidator<SeedDto> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SeedInteractor> _logger;

    public SeedInteractor(
        IUnitOfWork unitOfWork,
        IAccountRepository accountRepository,
        ISeedOutputPort outputPort,
        IInputPortValidator<SeedDto> validator,
        ILogger<SeedInteractor> logger
    )
	{
        _accountRepository = accountRepository;
        _outputPort = outputPort;
        _validator = validator;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
	
	public async Task Handle(SeedDto entityDto)
	{
        _logger.LogInformation("Iniciando proceso de seed");

        bool IsValid = await _validator.IsValid(entityDto);
        if (!IsValid)
        {
            await _outputPort.HandleFailure(HandleFailure.Fail(
                messages: _validator.Messages!,
                statusCode: _validator.HttpStatusCode));

            return;
        }

        var countAccounts = await  _accountRepository.GetAccountAllAsync(1,10,null);

        #region "Cuentas iniciales"
        if (countAccounts.TotalCount < 1)
        {
            Account account = new Account(
            userName: "administrator",
            email: "administrator@KFC.pe",
            accountType: AccountTypeEnum.Administrator
       );
            account.setPassword(PasswordHasher.HashPassword("Passw0rd$"));
            account.setEnable();

            await _accountRepository.CreateAsync(account);
        }
        #endregion
       
        await _unitOfWork.SaveChanges();
        await _outputPort.HandleSuccess(HandleSuccess<string>.Ok("Se inicio la configuracion Seed"));   
        
	}
}