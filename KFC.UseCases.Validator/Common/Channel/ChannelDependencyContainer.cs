using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KFC.UseCases.Validator;

public static class ChannelDependencyContainer
{
    public static IServiceCollection AddChannelValidators(this IServiceCollection services)
    {
        services.AddTransient<IInputPortValidator<CreateChannelDto>, CreateChannelValidator>();
        services.AddTransient<IInputPortValidator<DeleteChannelDto>, DeleteChannelValidator>();
        services.AddTransient<IInputPortValidator<UpdateChannelDto>, UpdateChannelValidator>();

        services.AddTransient<IInputPortValidator<GetChannelAllDto>, GetChannelAllValidator>();
        services.AddTransient<IInputPortValidator<GetChannelByCodeDto>, GetChannelByCodeValidator>();

        return services;
    }
}
