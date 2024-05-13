using System.Collections.Immutable;
using TradingApp.Module.Quotes.Contract.Constants;

namespace TradingApp.Module.Quotes.Application.Mappers;

public class TechnicalIndicatorMapper
{
    public static IImmutableSet<TechnicalIndicator> ToDomainModel(string[] technicalIndicators)
    {
        return technicalIndicators
            .Select(
                x =>
                    Enum.TryParse<TechnicalIndicator>(x, out var parsed)
                        ? parsed
                        : TechnicalIndicator.Rsi
            )
            .ToImmutableHashSet();
    }
}
