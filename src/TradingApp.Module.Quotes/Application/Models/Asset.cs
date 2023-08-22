using TradingApp.Module.Quotes.Domain.Enums;

namespace TradingApp.Module.Quotes.Application.Models;

public record Asset(AssetName Name, AssetType Type);