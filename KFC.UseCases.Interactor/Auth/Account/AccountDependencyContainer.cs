using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;

namespace KFC.UseCases.Interactor;

public static class AccountDependencyContainer
{
	public static IServiceCollection AddAccountInteractors(this IServiceCollection services)
	{

		services.AddTransient<ICreateAccountInputPort, CreateAccountInteractor>();
		services.AddTransient<IUpdateAccountInputPort, UpdateAccountInteractor>();
		services.AddTransient<IUpdatePasswordAccountInputPort, UpdatePasswordAccountInteractor>();
		services.AddTransient<IAccountActivateInputPort, AccountActivateInteractor>();
		services.AddTransient<IEnableAccountInputPort, EnableAccountInteractor>();
		services.AddTransient<IDisableAccountInputPort, DisableAccountInteractor>();
        
		services.AddTransient<IGetAccountByUserNameInputPort, GetAccountByUserNameInteractor>();
		services.AddTransient<IGetAccountAllInputPort, GetAccountAllInteractor>();
		services.AddTransient<IGetAccountByUserNameAndTokenInputPort, GetAccountByUserNameAndTokenAndTokenInteractor>();
		
		services.AddTransient<IGetAccountSessionInputPort, GetAccountSessionInteractor>();

		return services;
	}
}