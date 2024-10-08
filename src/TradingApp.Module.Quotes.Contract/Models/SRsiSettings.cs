﻿namespace TradingApp.Module.Quotes.Application.Models;

/// <summary>
/// 
/// </summary>
/// <param name="Enabled"></param>
/// <param name="ChannelLength">smooth period</param>
/// <param name="StochKSmooth">Period %K</param>
/// <param name="StochDSmooth">Period %D</param>
/// <param name="Oversold"></param>
/// <param name="Overbought"></param>
public record struct SrsiSettings(bool Enabled, int ChannelLength, int StochKSmooth, int StochDSmooth, decimal Oversold, decimal Overbought);
