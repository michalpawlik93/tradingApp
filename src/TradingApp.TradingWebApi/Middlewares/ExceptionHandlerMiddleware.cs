using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;
using TradingApp.Application.Models;

namespace TradingApp.TradingWebApi.Middlewares;


/// <summary>
/// Middleware for custom exception handling
/// </summary>
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private const string ApplicationJson = "application/json";
    private static JsonSerializerOptions _serializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var exHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exHandlerFeature?.Error != null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = ApplicationJson;
            var response = new ServiceResponse(exHandlerFeature.Error);
            _logger.LogError("Exception occured", exHandlerFeature.Error);
            await context.Response.WriteAsync(JsonSerializer.Serialize(response, _serializerOptions));
            return;
        }
        await _next(context);
    }
}

public static class ExceptionHandlerMiddlewareExtension
{
    public static IApplicationBuilder UseExceptionHandlerMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}

