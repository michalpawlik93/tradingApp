using TradingApp.Module.Quotes.Application.Models;

namespace TradingApp.Module.Quotes.Domain.Constants;

public static class SRsiSettingsConst
{
    public const decimal Overbought = 80;
    public const decimal Oversold = 20;
    public const int ChannelLength = 14;
    public const int StochKSmooth = 3;
    public const int StochDSmooth = 3;

    public static readonly SRsiSettings SRsiSettingsDefault = new(true, ChannelLength, StochKSmooth, StochDSmooth, Oversold, Overbought);
    /// <summary>
    /// Low level of false signals, filter noises, slow reaction
    /// </summary>
    public static readonly SRsiSettings SlowSrsiSettings = new(true, 3, 5, 3, Oversold, Overbought);
    /// <summary>
    /// High level of false signals, fast reaction
    /// </summary>
    public static readonly SRsiSettings FastSrsiSettings = new(true, 1, 5, 3, Oversold, Overbought);
    /// <summary>
    /// Time frame: M5, M15, M30
    /// (C,K,D) - (3,10,7) (3,7,3) (3,5,3)
    /// </summary>
    public static readonly SRsiSettings MinutesFrameSrsiSettings = new(true, 3, 7, 3, Oversold, Overbought);
    /// <summary>
    /// Time frame: H1, H4, 1D
    /// (C,K,D) - (3,9,3) (3,14,3) (3,21,3)
    /// </summary>
    public static readonly SRsiSettings HoursFrameSrsiSettings = new(true, 3, 14, 3, Oversold, Overbought);
    /// <summary>
    /// Time frame: Week, Days
    /// (C,K,D) - (7,21,7) (14,21,14)
    /// </summary>
    public static readonly SRsiSettings DaysFrameSrsiSettings = new(true, 7, 21, 7, Oversold, Overbought);
}
