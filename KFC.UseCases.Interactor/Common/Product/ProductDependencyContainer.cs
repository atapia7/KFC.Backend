using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.Interactor;

public static class ProductDependencyContainer
{
    public static IServiceCollection AddProductInteractors(this IServiceCollection services)
    {
        services.AddTransient<ICreateProductInputPort, CreateProductInteractor>();
        services.AddTransient<IDeleteProductInputPort, DeleteProductInteractor>();
        services.AddTransient<IUpdateProductInputPort, UpdateProductInteractor>();
        services.AddTransient<IGetProductByCodeInputPort, GetProductByCodeInteractor>();
        services.AddTransient<IGetProductAllInputPort, GetProductAllInteractor>();
        return services;
    }
}
