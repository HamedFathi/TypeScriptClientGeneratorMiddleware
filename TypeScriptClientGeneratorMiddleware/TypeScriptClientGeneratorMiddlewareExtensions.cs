using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TypeScriptClientGeneratorMiddleware;

public static class TypeScriptClientGeneratorMiddlewareExtensions
{
    public static IServiceCollection AddTypeScriptClientGenerator(this IServiceCollection service, Action<TypeScriptClientGeneratorOptions>? options = default)
    {
        options ??= _ => { };
        service.Configure(options);
        return service;
    }

    public static IApplicationBuilder UseTypeScriptClientGenerator(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TypeScriptClientGeneratorMiddleware>();
    }
}