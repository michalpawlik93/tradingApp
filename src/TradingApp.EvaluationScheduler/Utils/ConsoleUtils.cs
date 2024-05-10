using FluentResults;

namespace TradingApp.EvaluationScheduler.Utils;

public static class ConsoleUtils
{
    public static async Task WriteResultMessages(IResultBase result)
    {
        if (result.Errors is null) return;
        foreach (var message in result.Errors)
        {
            await Console.Out.WriteLineAsync($"Error: {message.Message}");
        }
    }
}
