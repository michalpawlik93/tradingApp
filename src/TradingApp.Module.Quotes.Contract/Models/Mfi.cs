namespace TradingApp.Module.Quotes.Contract.Models;

public record MfiResult(decimal Mfi);

public record struct MfiSettings(int ChannelLength, int ScaleFactor);
