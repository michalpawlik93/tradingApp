using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class AssetDtoMapper
{
    public static Asset ToDomainModel(AssetDto dto) => new(MapName(dto.Name), MapType(dto.Type));

    public static Asset ToDomainModel(AssetAsParamsDto dto) =>
        new(MapName(dto.AssetName), MapType(dto.AssetType));

    private static AssetName MapName(string name) =>
        Enum.TryParse<AssetName>(name, out var assetName) ? assetName : AssetName.BTC;

    private static AssetType MapType(string type) =>
        Enum.TryParse<AssetType>(type, out var assetType) ? assetType : AssetType.Cryptocurrency;
}
