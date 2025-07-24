using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.Interactor;

public static class PriceChannelDependencyContainer
{
    public static IServiceCollection AddPriceChannelInteractors(this IServiceCollection services)
    {
        services.AddTransient<ICreatePriceChannelInputPort, CreatePriceChannelInteractor>();
        services.AddTransient<IDeletePriceChannelInputPort, DeletePriceChannelInteractor>();
        services.AddTransient<IUpdatePriceChannelInputPort, UpdatePriceChannelInteractor>();
        services.AddTransient<IGetPriceChannelByCodeInputPort, GetPriceChannelByCodeInteractor>();
        services.AddTransient<IGetPriceChannelAllInputPort, GetPriceChannelAllInteractor>();
        return services;
    }
}
