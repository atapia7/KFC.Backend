using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;
using Microsoft.Extensions.DependencyInjection;


namespace KFC.UseCases.Validator;

public static class PriceChannelDependencyContainer
{
    public static IServiceCollection AddPriceChannelValidators(this IServiceCollection services)
    {
        services.AddTransient<IInputPortValidator<CreatePriceChannelDto>, CreatePriceChannelValidator>();
        services.AddTransient<IInputPortValidator<DeletePriceChannelDto>, DeletePriceChannelValidator>();
        services.AddTransient<IInputPortValidator<UpdatePriceChannelDto>, UpdatePriceChannelValidator>();

        services.AddTransient<IInputPortValidator<GetPriceChannelAllDto>, GetPriceChannelAllValidator>();
        services.AddTransient<IInputPortValidator<GetPriceChannelByCodeDto>, GetPriceChannelByCodeValidator>();

        return services;
    }
}
