using TradingApp.Modules.Domain.Enums;

namespace TradingApp.Modules.Application.Models;

public record Asset(AssetName Name, AssetType Type);