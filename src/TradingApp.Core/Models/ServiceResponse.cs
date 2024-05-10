using FluentResults;
using System.Diagnostics.CodeAnalysis;
using TradingApp.Core.Enums;
using TradingApp.Core.Extensions;

namespace TradingApp.Core.Models;

/// <summary>
/// Service response data
/// </summary>
[ExcludeFromCodeCoverage]
public class ServiceResponse
{
    protected ServiceResponse() { }

    public ServiceResponse(Exception exception)
    {
        Messages = GetServiceResponseMessages(exception);
    }

    public ServiceResponse(Result result)
    {
        Messages = GetServiceResponseMessages(result);
    }

    /// <summary>
    /// The service response DTO
    /// </summary>
    public string Data { get; set; }

    /// <summary>
    /// List of <see cref="ServiceResponseMessage"/> associated to the service response
    /// </summary>
    public List<ServiceResponseMessage> Messages { get; }

    private static List<ServiceResponseMessage> GetServiceResponseMessages(Exception exception)
    {
        return exception switch
        {
            _ => GetInternalServerErrorMessages()
        };
    }

    private static List<ServiceResponseMessage> GetServiceResponseMessages(Result result)
    {
        if (result.IsFailed)
        {
            return result.Errors
                .Select(error => new ServiceResponseMessage(error.Message, MessageType.Error))
                .ToList();
        }
        return new();
    }

    private static List<ServiceResponseMessage> GetInternalServerErrorMessages()
    {
        return new()
        {
            new()
            {
                Message = "Internal server exception. Please, contact your provider.",
                Type = MessageType.Error
            }
        };
    }
}

/// <summary>
/// Service response data
/// </summary>
[ExcludeFromCodeCoverage]
public class ServiceResponse<T>
{
    public ServiceResponse(IResult<T> result)
    {
        Messages = GetServiceResponseMessages(result);
        Data = result.ValueOrDefault;
    }

    /// <summary>
    /// The service response DTO
    /// </summary>
    public T Data { get; }

    /// <summary>
    /// List of <see cref="ServiceResponseMessage"/> associated to the service response
    /// </summary>
    public List<ServiceResponseMessage> Messages { get; }

    private static List<ServiceResponseMessage> GetServiceResponseMessages(IResult<T> result) =>
        result.IsFailed
            ? result.Errors
                .Select(
                    error =>
                        new ServiceResponseMessage(
                            error.Message,
                            error.GetErrorServiceResponseMessage()
                        )
                )
                .ToList()
            : result.Successes
                .Select(success => new ServiceResponseMessage(success.Message, MessageType.Success))
                .ToList();
}
