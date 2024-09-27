using System.Collections.Immutable;
using TradingApp.Module.Quotes.Application.Dtos;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class IndicatorsMapper
{
    public static ImmutableArray<Indicators> ToDomainModel(IndicatorsDto[] indicators) =>
        indicators
            .Select(
                x =>
                    new Indicators(
                        ToDomainModel(x.TechnicalIndicator),
                        ToDomainModel(x.SideIndicators)
                    )
            )
            .ToImmutableArray();

    public static IImmutableSet<SideIndicator> ToDomainModel(string[] sideIndicators)
    {
        return sideIndicators
            .Select(
                x =>
                    Enum.TryParse<SideIndicator>(x, out var parsed)
                        ? parsed
                        : SideIndicator.Ema2x
            )
            .ToImmutableHashSet();
    }

    public static TechnicalIndicator ToDomainModel(string technicalIndicator) =>
        Enum.TryParse<TechnicalIndicator>(technicalIndicator, out var parsed)
            ? parsed
            : TechnicalIndicator.Rsi;
}
