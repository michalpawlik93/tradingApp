using TradingApp.Module.Quotes.Contract.Models;

namespace TradingApp.Module.Quotes.Application.Models;


public record GetQuotesRequest(TimeFrame TimeFrame, Asset Asset, PostProcessing? PostProcessing = null);
