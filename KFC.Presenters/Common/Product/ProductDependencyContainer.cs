using Microsoft.Extensions.DependencyInjection;
using KFC.UseCases.OutputPort;

namespace KFC.Presenters;

public static class ProductDependencyContainer
{
	public static IServiceCollection AddProductPresenters(this IServiceCollection services)
	{
		services.AddScoped<IGetProductByCodeOutputPort, GetProductByCodePresenter>();
		services.AddScoped<IGetProductAllOutputPort, GetProductAllPresenter>();
		
		services.AddScoped<ICreateProductOutputPort, CreateProductPresenter>();
		services.AddScoped<IUpdateProductOutputPort, UpdateProductPresenter>();
		services.AddScoped<IDeleteProductOutputPort, DeleteProductPresenter>();

		return services;
	}
}