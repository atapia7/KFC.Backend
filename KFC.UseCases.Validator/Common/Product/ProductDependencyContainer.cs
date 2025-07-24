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

public static class ProductDependencyContainer
{
    public static IServiceCollection AddProductValidators(this IServiceCollection services)
    {
        services.AddTransient<IInputPortValidator<CreateProductDto>, CreateProductValidator>();
        services.AddTransient<IInputPortValidator<DeleteProductDto>, DeleteProductValidator>();
        services.AddTransient<IInputPortValidator<UpdateProductDto>, UpdateProductValidator>();

        services.AddTransient<IInputPortValidator<GetProductAllDto>, GetProductAllValidator>();
        services.AddTransient<IInputPortValidator<GetProductByCodeDto>, GetProductByCodeValidator>();

        return services;
    }
}
