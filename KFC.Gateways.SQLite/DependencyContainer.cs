using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using KFC.UseCases.Interface;
using Microsoft.EntityFrameworkCore;

namespace KFC.Gateways.SQLite;

public static class DependencyContainer
{
	public static IServiceCollection AddRepositoriesSQLite(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationContext>(options =>
			options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

		services.AddScoped<IAccountRepository, AccountRepository>();
		services.AddScoped<IChannelRepository, ChannelRepository>();
		services.AddScoped<IPriceChannelRepository, PriceChannelRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // No necesitas registrar ILogger manualmente, el framework lo hace automáticamente
        return services;

	}

}