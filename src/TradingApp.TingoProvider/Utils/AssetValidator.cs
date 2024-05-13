using FluentResults;
using TradingApp.Core.Models;
using TradingApp.Module.Quotes.Contract.Constants;
using TradingApp.Module.Quotes.Contract.Models;
using TradingApp.TingoProvider.Contstants;
using TradingApp.TingoProvider.Mappers;

namespace TradingApp.TingoProvider.Utils;

public static class AssetValidator
{
    public static bool IsValid(this Asset asset, out Result result)
    {
        Func<Asset, Result>[] rules =
        [
            AssetTypeRule,
            AssetNameRule
        ];
        var errors = new List<ValidationError>();
        foreach (var rule in rules)
        {
            if (rule(asset).HasError<ValidationError>(out var ruleErrors))
            {
                errors.AddRange(ruleErrors);
            }
        }
        result = errors.Count != 0 ? Result.Fail(errors) : Result.Ok();
        return result.IsSuccess;
    }
    public const string AssetTypeRuleMessage = "Only cryptocurrency is supported";
    public const string AssetNameRuleMessage = "Provided ticker is incorrect";
    private static Result AssetTypeRule(Asset asset) =>
        asset.Type != AssetType.Cryptocurrency ? Result.Fail(new ValidationError(AssetTypeRuleMessage)) : Result.Ok();
    private static Result AssetNameRule(Asset asset) =>
        !Ticker.Tickers.Contains(asset.Map()) ? Result.Fail(new ValidationError(AssetNameRuleMessage)) : Result.Ok();
}

