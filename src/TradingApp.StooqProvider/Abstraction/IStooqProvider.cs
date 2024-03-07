using FluentResults;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.StooqProvider.Abstraction;

public interface IStooqProvider
{
    Task<Result<ICollection<Quote>>> GetQuotesAsync(TimeFrame timeFrame, Asset asset);
}
