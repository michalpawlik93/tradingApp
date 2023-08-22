using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Models;

public record TimeFrame(Granularity Granularity, DateTime? StartDate, DateTime? EndDate);