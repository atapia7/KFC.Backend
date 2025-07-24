using Microsoft.Extensions.DependencyInjection;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public static class AccountDependencyContainer
{
	public static IServiceCollection AddAccountPresenters(this IServiceCollection services)
	{
		services.AddScoped<ICreateAccountOutputPort, CreateAccountPresenter>();
		services.AddScoped<ICreateAccountCustomerOutputPort, CreateAccountCustomerPresenter>();
		services.AddScoped<ICreateAccountDealerOutputPort, CreateAccountDealerPresenter>();
		services.AddScoped<IUpdateAccountOutputPort, UpdateAccountPresenter>();
		services.AddScoped<IUpdatePasswordAccountOutputPort, UpdatePasswordAccountPresenter>();
		services.AddScoped<IAccountActivateOutputPort, AccountActivatePresenter>();
		services.AddScoped<ISendMailAccountActivateOutputPort, SendMailAccountActivatePresenter>();
		services.AddScoped<ISendAccountMailPasswordRecoveryOutputPort, SendAccountMailPasswordRecoveryPresenter>();
		services.AddScoped<IDisableAccountOutputPort, DisableAccountPresenter>();
		services.AddScoped<IEnableAccountOutputPort, EnableAccountPresenter>();

		services.AddScoped<IGetAccountByIdOutputPort, GetAccountByIdPresenter>();
		services.AddScoped<IGetAccountByUserNameOutputPort, GetAccountByUserNamePresenter>();
		services.AddScoped<IGetAccountAllOutputPort, GetAccountAllPresenter>();
		services.AddScoped<IGetSellersByAccountSessionOutputPort, GetSellersByAccountSessionPresenter>();
		services.AddScoped<IGetAccountSessionOutputPort, GetAccountSessionPresenter>();
		services.AddScoped<IGetAccountByUserNameAndTokenOutputPort, GetAccountByUserNameAndTokenPresenter>();

		return services;
	}
}