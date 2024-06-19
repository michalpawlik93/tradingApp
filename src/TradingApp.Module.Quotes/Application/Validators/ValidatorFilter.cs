using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace TradingApp.Module.Quotes.Application.Validators;

[ExcludeFromCodeCoverage]
public class ValidatorFilter<T> : IEndpointFilter
{
    public async ValueTask<object> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next
    )
    {
        var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();
        if (validator is null)
            return await next(context);
        var entity = context.Arguments.OfType<T>().FirstOrDefault(a => a?.GetType() == typeof(T));
        if (entity is not null)
        {
            var results = await validator.ValidateAsync((entity));
            if (!results.IsValid)
            {
                return Results.ValidationProblem(results.ToDictionary());
            }
        }
        else
        {
            return Results.Problem("Error Not Found");
        }

        return await next(context);
    }
}
