using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;


namespace KFC.UseCases.Validator;

public static class PriceChannelDependencyContainer
{
    public static IServiceCollection AddPriceChannelValidators(this IServiceCollection services)
    {
        services.AddTransient<IInputPortValidator<CreateChannelDto>, CreateChannelValidator>();
        services.AddTransient<IInputPortValidator<DeleteChannelDto>, DeleteChannelValidator>();
        services.AddTransient<IInputPortValidator<UpdateChannelDto>, UpdateChannelValidator>();

        services.AddTransient<IInputPortValidator<GetChannelAllDto>, GetChannelAllValidator>();
        services.AddTransient<IInputPortValidator<GetChannelByCodeDto>, GetChannelByCodeValidator>();

        return services;
    }
}
