using Microsoft.Extensions.DependencyInjection;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public static class PriceChannelDependencyContainer
{
	public static IServiceCollection AddPriceChannelPresenters(this IServiceCollection services)
	{
		services.AddScoped<IGetPriceChannelByCodeOutputPort, GetPriceChannelByCodePresenter>();
		services.AddScoped<IGetPriceChannelAllOutputPort, GetPriceChannelAllPresenter>();
		
		services.AddScoped<ICreatePriceChannelOutputPort, CreatePriceChannelPresenter>();
		services.AddScoped<IUpdatePriceChannelOutputPort, UpdatePriceChannelPresenter>();
		services.AddScoped<IDeletePriceChannelOutputPort, DeletePriceChannelPresenter>();

		return services;
	}
}