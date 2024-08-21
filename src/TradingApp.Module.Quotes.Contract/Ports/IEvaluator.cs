using FluentResults;
using TradingApp.Module.Quotes.Application.Models;
using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Contract.Ports;


public interface IEvaluator
{
    IReadOnlyList<RsiResult> GetRsi(
        IReadOnlyList<Quote> quotes,
        RsiSettings settings
    );
    IReadOnlyList<WaveTrendResult> GetWaveTrend(
        IReadOnlyList<Quote> quotes,
        WaveTrendSettings settings
    );
    IReadOnlyList<SRsiResult> GetSrsi(
        IReadOnlyList<Quote> quotes,
        SRsiSettings settings
    );
    IReadOnlyList<MfiResult> GetMfi(
        IReadOnlyList<Quote> quotes,
        MfiSettings settings
    );

    Result<decimal[]> GetEmea(decimal[] values, int length);
}
