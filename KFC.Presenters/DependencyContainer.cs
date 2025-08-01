using Microsoft.Extensions.DependencyInjection;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public static class DependencyContainer
{
	public static IServiceCollection AddPresenters(this IServiceCollection services)
	{

		services.AddAccountPresenters();
		services.AddChannelPresenters();
		services.AddPriceChannelPresenters();
		services.AddProductPresenters();
		
		services.AddScoped<ILoginOutputPort, LoginPresenter>();
		services.AddScoped<ISeedOutputPort, SeedPresenter>();

        return services;
	}
}