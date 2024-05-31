namespace TradingApp.Module.Quotes.Contract.Models;

public record MfiResult(decimal Mfi);

public record MfiSettings(int ChannelLength, int ScaleFactor);
