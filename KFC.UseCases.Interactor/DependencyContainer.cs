using KFC.UseCases.InputPort;
using KFC.UseCases.Service;
using Microsoft.Extensions.DependencyInjection;

namespace KFC.UseCases.Interactor;

public static class DependencyContainer
{
	public static IServiceCollection AddInteractors(this IServiceCollection services)
	{

		services.AddAccountInteractors();
		services.AddChannelInteractors();
		services.AddPriceChannelInteractors();
		services.AddProductInteractors();

        //Auth
        services.AddTransient<ILoginInputPort, LoginInteractor>();
		services.AddTransient<ISeedInputPort, SeedInteractor>();
		
		return services;
	}
}