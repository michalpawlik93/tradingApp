using TradingApp.TradingAdapter.Enums;

namespace TradingApp.TradingAdapter.Models;

public record TimeFrame(Granularity Granularity, DateTime? StartDate, DateTime? EndDate);