using TradingApp.Modules.Domain.Enums;

namespace TradingApp.Modules.Application.Models;

public record TimeFrame(Granularity Granularity, DateTime? StartDate, DateTime? EndDate);