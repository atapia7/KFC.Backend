using Microsoft.Extensions.DependencyInjection;

using KFC.UseCases.DTOs.Input;
using KFC.UseCases.InputPort;

namespace KFC.UseCases.Validator;

public static class AccountDependencyContainer
{
    public static IServiceCollection AddAccountValidators(this IServiceCollection services)
    {
      
        //Auth
        services.AddTransient<IInputPortValidator<CreateAccountDto>, CreateAccountValidator>();
        services.AddTransient<IInputPortValidator<UpdateAccountDto>, UpdateAccountValidator>();
        services.AddTransient<IInputPortValidator<UpdatePasswordAccountDto>, UpdatePasswordAccountValidator>();
        services.AddTransient<IInputPortValidator<AccountActivateDto>, AccountActivateValidator>();
        services.AddTransient<IInputPortValidator<EnableAccountDto>, EnableAccountValidator>();
        services.AddTransient<IInputPortValidator<DisableAccountDto>, DisableAccountValidator>();
        
        services.AddTransient<IInputPortValidator<GetAccountAllDto>, GetAccountAllValidator>();
        services.AddTransient<IInputPortValidator<GetAccountByIdDto>, GetAccountByIdValidator>();
        services.AddTransient<IInputPortValidator<GetAccountByUserNameDto>, GetAccountByUserNameValidator>();
        services.AddTransient<IInputPortValidator<GetAccountByUserNameAndTokenDto>, GetAccountByUserNameAndTokenValidator>();

        services.AddTransient<IInputPortValidator<GetSessionDto>, GetAccountSessionValidator>();
       
        return services;
    }
}
