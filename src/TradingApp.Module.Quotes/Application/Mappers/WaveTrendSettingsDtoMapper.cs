using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class WaveTrendSettingsDtoMapper
{
    public static WaveTrendSettings ToDomainModel(WaveTrendSettingsDto dto)
    {
        if (dto == null) return WaveTrendSettingsConst.WaveTrendSettingsDefault;
        return new WaveTrendSettings(
            dto.Oversold == 0 ? WaveTrendSettingsConst.Oversold : dto.Oversold,
            dto.Overbought == 0 ? WaveTrendSettingsConst.Overbought : dto.Overbought,
            dto.OversoldLevel2 == 0 ? WaveTrendSettingsConst.OversoldLevel2 : dto.OversoldLevel2,
            dto.OverboughtLevel2 == 0 ? WaveTrendSettingsConst.OverboughtLevel2 : dto.OverboughtLevel2,
            dto.ChannelLength == default ? WaveTrendSettingsConst.ChannelLength : dto.ChannelLength,
            dto.AverageLength == default ? WaveTrendSettingsConst.AverageLength : dto.AverageLength,
            dto.MovingAverageLength == default ? WaveTrendSettingsConst.MovingAverageLength : dto.MovingAverageLength
        );
    }

}
