import { rsiSettingsDefault } from "../consts/technicalIndicatorsSettings";
import { RsiChartData } from "../types/ChartData";
import { RsiQuote } from "../types/RsiQuote";
import { RsiSettings } from "../types/RsiSettings";

export function mapToRsiChartData(rsiQuotes: RsiQuote[], rsiSettings: RsiSettings): RsiChartData {
  const result: RsiChartData = {
    ...rsiSettingsDefault,
    rsi: [],
  };
  result.overbought = rsiSettings.overbought;
  result.oversold = rsiSettings.oversold;
  for (const quote of rsiQuotes) {
    const timestamp = Date.parse(quote.ohlc.date);
    if (!Number.isNaN(timestamp)) {
      const x = new Date(quote.ohlc.date).getTime();
      result.rsi.push([x, quote.rsi]);
    }
  }
  return result;
}
