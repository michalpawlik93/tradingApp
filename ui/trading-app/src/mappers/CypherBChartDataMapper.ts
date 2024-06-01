import { CypherBChartData } from "../types/ChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToCypherBChartData(quotes: CypherBQuote[]): CypherBChartData {
  const result: CypherBChartData = {
    waveTrendWt1: [],
    waveTrendWt2: [],
    waveTrendVwap: [],
    sellSignals: [],
    buySignals: [],
    mfi: [],
  };

  quotes.forEach((quote) => {
    const timestamp = Date.parse(quote.ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quote.ohlc.date).getTime();
      const { waveTrend, mfi } = quote;

      if (waveTrend !== null) {
        result.waveTrendWt1.push([x, waveTrend.wt1]);
        result.waveTrendWt2.push([x, waveTrend.wt2]);
        if (waveTrend.vwap) {
          result.waveTrendVwap.push([x, waveTrend.vwap]);
        }
        if (waveTrend.crossesOver) {
          result.sellSignals.push([x, waveTrend.wt1]);
        }
        if (waveTrend.crossesUnder) {
          result.buySignals.push([x, waveTrend.wt1]);
        }
      }

      if (mfi !== null) {
        result.mfi.push([x, mfi.mfi]);
      }
    }
  });

  return result;
}
