using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class SRsiSettingsDtoMapper
{
    public static SrsiSettings? ToDomainModel(SrsiSettingsDto dto) =>
        dto != null
            ? new SrsiSettings(
                dto.Enabled,
                dto.ChannelLength,
                dto.StochKSmooth,
                dto.StochDSmooth,
                dto.Oversold,
                dto.Overbought
            )
            : null;
}
