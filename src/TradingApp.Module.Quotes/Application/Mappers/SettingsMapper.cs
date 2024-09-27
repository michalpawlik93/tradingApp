using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class SettingsMapper
{
    public static SettingsRequest ToDomainModel(SettingsDto settings) =>
        new(
            ToDomainModel(settings.SrsiSettings),
            ToDomainModel(settings.RsiSettings),
            ToDomainModel(settings.MfiSettings),
            ToDomainModel(settings.WaveTrendSettings)
        );

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

    public static RsiSettings? ToDomainModel(RsiSettingsDto dto) =>
        dto != null
            ? new RsiSettings(dto.Oversold, dto.Overbought, dto.Enabled, dto.ChannelLength)
            : null;

    public static MfiSettings? ToDomainModel(MfiSettingsDto dto) =>
        dto != null ? new MfiSettings(dto.ChannelLength, dto.ScaleFactor) : null;

    public static WaveTrendSettings ToDomainModel(WaveTrendSettingsDto dto)
    {
        if (dto == null)
            return WaveTrendSettingsConst.WaveTrendSettingsDefault;
        return new WaveTrendSettings(
            dto.Oversold == 0 ? WaveTrendSettingsConst.Oversold : dto.Oversold,
            dto.Overbought == 0 ? WaveTrendSettingsConst.Overbought : dto.Overbought,
            dto.OversoldLevel2 == 0 ? WaveTrendSettingsConst.OversoldLevel2 : dto.OversoldLevel2,
            dto.OverboughtLevel2 == 0
                ? WaveTrendSettingsConst.OverboughtLevel2
                : dto.OverboughtLevel2,
            dto.ChannelLength == default ? WaveTrendSettingsConst.ChannelLength : dto.ChannelLength,
            dto.AverageLength == default ? WaveTrendSettingsConst.AverageLength : dto.AverageLength,
            dto.MovingAverageLength == default
                ? WaveTrendSettingsConst.MovingAverageLength
                : dto.MovingAverageLength
        );
    }
}
