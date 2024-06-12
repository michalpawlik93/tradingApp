using TradingApp.Domain.Modules.Constants;
using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class GetCombinedQuotesResponseMapper
{
    public static GetCombinedQuotesResponseDto ToDto(
        IEnumerable<Quote> quotes,
        IEnumerable<RsiResult> rsiResults,
        bool includeRsi
    ) =>
        new(
            ToCombinedResults(quotes, rsiResults, includeRsi),
            includeRsi
                ? new RsiSettings(RsiSettingsConst.Oversold, RsiSettingsConst.Overbought, true, 14)
                : null
        );

    public static IEnumerable<CombinedQuote> ToCombinedResults(
        IEnumerable<Quote> rawQuotes,
        IEnumerable<RsiResult> rsiResults,
        bool includeRsi
    )
    {
        if (includeRsi)
        {
            return rawQuotes
                .Select((q, i) => new CombinedQuote(q, rsiResults.ElementAt(i).Value, null))
                .ToList();
        }
        var combinedResults = rawQuotes.Select((q, i) => new CombinedQuote(q, null, null)).ToList();
        return combinedResults;
    }
}
