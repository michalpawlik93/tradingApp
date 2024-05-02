using FluentResults;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.TingoProvider.Contract;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Mappers;
using TradingApp.TingoProvider.Setup;

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

    public async Task<Result<IEnumerable<Quote>>> GetQuotes(TimeFrame timeFrame, Asset asset, CancellationToken cancellationToken)
    {
        if (asset.Type != AssetType.Cryptocurrency)
        {
            return Result.Fail("Only cryptocurrency is supported");
        }

        var ticker = asset.Map();
        if (!Ticker.Tickers.Contains(ticker))
        {
            return Result.Fail("Provide correct ticker");
        }
        var response = await _tingoClient.Client.GetAsync(UrlMapper.GetCryptoQuotesUri(ticker, timeFrame), cancellationToken);
        var result = await response.GetResultAsync<TingoQuote[]>();
        return result.IsSuccess ? result.ToResult(TingoQuoteMapper.MapToQuotes) : result.ToResult<IEnumerable<Quote>>();
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



