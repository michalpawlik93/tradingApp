import { rsiSettingsDefault } from "../consts/technicalIndicatorsSettings";
import { RsiChartData } from "../types/ChartData";
import { CombinedQuote } from "../types/CombinedQuote";
import { RsiSettings } from "../types/RsiSettings";

export function mapToRsiChartData(
  combinedQuotes: CombinedQuote[],
  rsiSettings: RsiSettings,
): RsiChartData {
  const result: RsiChartData = {
    ...rsiSettingsDefault,
    rsi: [],
  };
  result.overbought = rsiSettings.overbought;
  result.oversold = rsiSettings.oversold;
  for (const combinedQuote of combinedQuotes) {
    const timestamp = Date.parse(combinedQuote.ohlc.date);
    if (!Number.isNaN(timestamp)) {
      const x = new Date(combinedQuote.ohlc.date).getTime();
      result.rsi.push([x, combinedQuote.rsi]);
    }
  }
  return result;
}
