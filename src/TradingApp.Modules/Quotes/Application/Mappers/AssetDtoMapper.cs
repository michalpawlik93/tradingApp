using TradingApp.Modules.Quotes.Application.Models;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Modules.Quotes.Application.Mappers;

public static class AssetDtoMapper
{
    public static Asset ToDomainModel(AssetDto dto)
    {
        return new Asset(
            Enum.TryParse<AssetName>(dto.Name, out var assetNameParsed)
                ? assetNameParsed
                : AssetName.BTC,
            Enum.TryParse<AssetType>(dto.Type, out var assetTypeParsed)
                ? assetTypeParsed
                : AssetType.Cryptocurrency
        );
    }
}
