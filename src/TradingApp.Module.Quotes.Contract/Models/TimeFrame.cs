using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Contract.Models;

public record TimeFrame(Granularity Granularity, DateTime? StartDate, DateTime? EndDate);
