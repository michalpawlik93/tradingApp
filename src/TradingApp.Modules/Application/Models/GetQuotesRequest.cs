namespace TradingApp.Modules.Application.Models;


public record GetQuotesRequest(TimeFrame TimeFrame, Asset Asset, PostProcessing? PostProcessing = null);
