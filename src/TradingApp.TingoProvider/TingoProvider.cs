using FluentResults;
using TradingApp.Core.Utilities;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.Module.Quotes.Contract.Ports;
using TradingApp.TingoProvider.Contract;
using TradingApp.TingoProvider.Mappers;
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

    public async Task<Result<IReadOnlyList<Quote>>> GetQuotes(TimeFrame timeFrame, Asset asset, CancellationToken cancellationToken)
    {
        if (!asset.IsValid(out var validationResult))
        {
            return validationResult;
        }
        var response = await _tingoClient.Client.GetAsync(UrlMapper.GetCryptoQuotesUri(asset, timeFrame), cancellationToken);
        var result = await response.GetResultAsync<TingoQuote[]>();
        return result.IsSuccess ? result.ToResult(TingoQuoteMapper.MapToQuotes) : result.ToResult<IReadOnlyList<Quote>>();
    }

    public async Task<Result<CryptocurrencyMetadata[]>> GetTickerMetadata(Asset asset, CancellationToken cancellationToken)
    {
        if (!asset.IsValid(out var validationResult))
        {
            return validationResult;
        }

        var response = await _tingoClient.Client.GetAsync(UrlMapper.GetTickerMetadataUri(asset), cancellationToken);
        return await response.GetResultAsync<CryptocurrencyMetadata[]>();
    }
}



