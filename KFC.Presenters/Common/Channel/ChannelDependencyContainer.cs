using Microsoft.Extensions.DependencyInjection;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public static class ChannelDependencyContainer
{
	public static IServiceCollection AddChannelPresenters(this IServiceCollection services)
	{
		services.AddScoped<IGetChannelByCodeOutputPort, GetChannelByCodePresenter>();
		services.AddScoped<IGetChannelAllOutputPort, GetChannelAllPresenter>();
		
		services.AddScoped<ICreateChannelOutputPort, CreateChannelPresenter>();
		services.AddScoped<IUpdateChannelOutputPort, UpdateChannelPresenter>();
		services.AddScoped<IDeleteChannelOutputPort, DeleteChannelPresenter>();

		return services;
	}
}