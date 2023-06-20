import { ApexRsiChartData } from "../types/ApexRsiChartData";
import { CombinedQuote } from "../types/CombinedQuote";
import { RsiSettings } from "../types/RsiSettings";

export function mapToApexRsiChartData(
  combinedQuotes: CombinedQuote[],
  rsiSettings: RsiSettings
): ApexRsiChartData {
  const result: ApexRsiChartData = {
    overbought: [],
    oversold: [],
    rsi: [],
  };
  for (let i = 0; i < combinedQuotes.length; i++) {
    const timestamp = Date.parse(combinedQuotes[i].ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(combinedQuotes[i].ohlc.date);
      result.overbought.push({ y: rsiSettings.overbought, x: x });
      result.oversold.push({ y: rsiSettings.oversold, x: x });
      result.rsi.push({ y: combinedQuotes[i].rsi, x: x });
    }
  }
  return result;
}
