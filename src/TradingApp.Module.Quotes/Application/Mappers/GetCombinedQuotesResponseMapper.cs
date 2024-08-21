using TradingApp.Module.Quotes.Application.Features.GetCombinedQuotes.Dto;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Mappers;

public static class GetCombinedQuotesResponseMapper
{
    public static GetCombinedQuotesResponseDto ToDto(
        IReadOnlyList<Quote> quotes,
        IReadOnlyList<RsiResult> rsiResults,
        IReadOnlyList<SrsiSignal> srsiSignals
    ) =>
        new(
            quotes
                .Select(
                    (q, i) =>
                        new CombinedQuote(
                            q,
                            rsiResults?.ElementAtOrDefault(i)?.Value,
                            srsiSignals?.ElementAtOrDefault(i)
                        )
                )
                .ToList()
        );
}
