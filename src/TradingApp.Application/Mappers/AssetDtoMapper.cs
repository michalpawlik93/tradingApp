using TradingApp.Application.Models;
using TradingApp.TradingAdapter.Enums;
using TradingApp.TradingAdapter.Models;

namespace TradingApp.Application.Mappers;

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
