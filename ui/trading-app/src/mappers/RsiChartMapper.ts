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
  for (let i = 0; i < combinedQuotes.length; i++) {
    const timestamp = Date.parse(combinedQuotes[i].ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(combinedQuotes[i].ohlc.date).getTime();
      result.rsi.push([x, combinedQuotes[i].rsi]);
    }
  }
  return result;
}
