using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.Interactor;

public static class ChannelDependencyContainer
{
    public static IServiceCollection AddChannelInteractors(this IServiceCollection services)
    {
        services.AddTransient<ICreateChannelInputPort, CreateChannelInteractor>();
        services.AddTransient<IDeleteChannelInputPort, DeleteChannelInteractor>();
        services.AddTransient<IUpdateChannelInputPort, UpdateChannelInteractor>();
        services.AddTransient<IGetChannelByCodeInputPort, GetChannelByCodeInteractor>();
        services.AddTransient<IGetChannelAllInputPort, GetChannelAllInteractor>();
        return services;
    }
}
