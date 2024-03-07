using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class WaveTrendSettingsDtoMapper
{
    public static WaveTrendSettings ToDomainModel(WaveTrendSettingsDto dto)
    {
        return new WaveTrendSettings(dto.Oversold, dto.Overbought, dto.ChannelLength, dto.AverageLength, dto.MovingAverageLength);
    }
}
