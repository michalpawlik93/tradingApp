namespace TradingApp.TradingAdapter.Models;


public record GetQuotesRequest(TimeFrame TimeFrame, Asset Asset, PostProcessing? PostProcessing = null);
