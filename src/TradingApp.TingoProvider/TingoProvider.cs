using FluentResults;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Setup;
using TradingApp.TingoProvider.Utils;

namespace TradingApp.TingoProvider;

public interface ITingoProvider : ITradingProvider;

//https://www.tiingo.com/documentation/crypto
public class TingoProvider : ITingoProvider
{
    private readonly TingoClient _tingoClient;

    public TingoProvider(TingoClient tingoClient)
    {
        ArgumentNullException.ThrowIfNull(tingoClient);
        _tingoClient = tingoClient;
    }

    public Task<Result<IEnumerable<Quote>>> GetQuotes(TimeFrame TimeFrame, Asset Asset, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(string ticker, CancellationToken cancellationToken)
    {
        if (!Ticker.Tickers.Contains(ticker))
        {
            return Result.Fail("Provide correct ticker");
        }
        var response = await _tingoClient.Client.GetAsync($"tiingo/crypto?tickers={ticker}", cancellationToken);
        return await response.GetResultAsync<CryptocurrencyMetadata[]>();
    }
}
