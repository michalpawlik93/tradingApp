using FluentResults;
using TradingApp.Core.Models;

namespace TradingApp.EvaluationScheduler.Utils;

public static class ConsoleUtils
{
    public static async Task WriteMessages(ServiceResponse response)
    {
        if (response?.Messages != null)
        {
            foreach (var message in response.Messages)
            {
                await Console.Out.WriteLineAsync($"Message type: {message.Type}, {message.Message}");
            }
        }
    }

    public static async Task WriteResultMessages<T>(Result<T> result)
    {
        foreach (var message in result.Errors)
        {
            await Console.Out.WriteLineAsync($"Error: {message.Message}");
        }
    }
}
