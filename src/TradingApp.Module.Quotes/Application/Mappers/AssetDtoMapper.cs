using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

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
