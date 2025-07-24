using Microsoft.Extensions.DependencyInjection;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;

namespace KFC.UseCases.Validator;

public static class DependencyContainer
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddAccountValidators();
        services.AddProductValidators();
        services.AddChannelValidators();
        services.AddPriceChannelValidators();

        services.AddTransient<IInputPortValidator<JwtAuthorization>, JwtValidator>();
        services.AddTransient<IInputPortValidator<LoginDto>, LoginValidator>();
        services.AddTransient<IInputPortValidator<SeedDto>, SeedValidator>();

        return services;
    }
}
