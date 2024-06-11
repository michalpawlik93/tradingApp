import { OhlcChartData } from "../types/ChartData";
import { Quote } from "../types/Quote";

export function mapToOhlcChartData(quotes: Quote[]): OhlcChartData {
  const result: OhlcChartData = {
    ohlc: [],
  };

  quotes.forEach((quote) => {
    const timestamp = Date.parse(quote.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quote.date).getTime();
      result.ohlc.push([x, quote.open, quote.close, quote.low, quote.high]);
    }
  });

  return result;
}
