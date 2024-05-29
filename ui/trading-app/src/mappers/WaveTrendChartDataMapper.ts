import { CypherBQuote } from "../types/CypherBQuote";
import { WaveTrendChartData } from "../types/WaveTrendChartData";

export function mapToWaveTrendChartData(quotes: CypherBQuote[]): WaveTrendChartData {
  const result: WaveTrendChartData = {
    waveTrendWt1: [],
    waveTrendWt2: [],
    sellSignals: [],
    buySignals: [],
  };

  quotes.forEach((quote) => {
    const timestamp = Date.parse(quote.ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quote.ohlc.date).getTime();
      const { waveTrend } = quote;

      if (waveTrend !== null) {
        result.waveTrendWt1.push([x, waveTrend.wt1]);
        result.waveTrendWt2.push([x, waveTrend.wt2]);
        if (waveTrend.crossesOver) {
          result.sellSignals.push([x, waveTrend.wt1]);
        }
        if (waveTrend.crossesUnder) {
          result.buySignals.push([x, waveTrend.wt1]);
        }
      }
    }
  });

  return result;
}
