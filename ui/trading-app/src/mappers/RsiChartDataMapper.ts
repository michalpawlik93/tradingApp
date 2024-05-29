import { RsiChartData } from "../types/ChartData";
import { CombinedQuote } from "../types/CombinedQuote";
import { RsiSettings } from "../types/RsiSettings";

export function mapToRsiChartData(
  combinedQuotes: CombinedQuote[],
  rsiSettings: RsiSettings,
): RsiChartData {
  const rsi: [string, number][] = [];

  combinedQuotes.forEach((quote) => {
    const date = quote.ohlc.date;
    rsi.push([date, quote.rsi]);
  });
  return { overbought: rsiSettings.overbought, oversold: rsiSettings.oversold, rsi };
}
