using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using NSwag.CodeGeneration.TypeScript;

namespace TypeScriptClientGeneratorMiddleware;

public class TypeScriptClientGeneratorMiddleware
{
    private readonly RequestDelegate _next;

    public TypeScriptClientGeneratorMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IOptions<TypeScriptClientGeneratorOptions> configureOptions)
    {
        foreach (var clientOptions in configureOptions.Value.Options)
        {
            if (context.Request.Path.Value == $"/{clientOptions.Endpoint.Trim('/')}" && context.Request.Method == "GET")
            {
                var document = NSwag.OpenApiDocument.FromUrlAsync(clientOptions.SwaggerJsonEndpoint).GetAwaiter().GetResult();
                var settings = clientOptions.Settings ?? new TypeScriptClientGeneratorSettings();
                var generator = new TypeScriptClientGenerator(document, settings);
                var source = generator.GenerateFile();
                var result = clientOptions.Process?.Invoke(source) ?? source;
                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = StatusCodes.Status200OK;
                await context.Response.WriteAsync(result);
            }
            else
            {
                await _next(context);
            }
        }
    }
}