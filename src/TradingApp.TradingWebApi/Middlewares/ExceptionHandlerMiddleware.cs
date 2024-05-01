using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;
using TradingApp.Core.Models;

namespace TradingApp.TradingWebApi.Middlewares;

/// <summary>
/// Middleware for custom exception handling
/// </summary>
public class ExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlerMiddleware> logger
    )
{
    private const string ApplicationJson = "application/json";
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };


    public async Task InvokeAsync(HttpContext context)
    {
        var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exHandlerFeature?.Error != null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = ApplicationJson;
            var response = new ServiceResponse(exHandlerFeature.Error);
            logger.LogError("Exception occured {err}", exHandlerFeature.Error);
            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, SerializerOptions)
            );
            return;
        }
        await next(context);
    }
}

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(
        this IApplicationBuilder builder
    ) => builder.UseMiddleware<ExceptionHandlerMiddleware>();
}
