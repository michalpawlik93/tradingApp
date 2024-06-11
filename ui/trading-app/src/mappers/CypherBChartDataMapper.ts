import { CypherBChartData } from "../types/ChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToCypherBChartData(quotes: CypherBQuote[]): CypherBChartData {
  const result: CypherBChartData = {
    waveTrendWt1: [],
    waveTrendWt2: [],
    waveTrendVwap: [],
    waveTrendSell: [],
    waveTrendBuy: [],
    mfiBuy: [],
    mfiSell: [],
    ohlc: [],
  };

  quotes.forEach((quote) => {
    const timestamp = Date.parse(quote.ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quote.ohlc.date).getTime();

      result.ohlc.push([x, quote.ohlc.open, quote.ohlc.close, quote.ohlc.low, quote.ohlc.high]);
      const { waveTrend, mfi } = quote;

      if (waveTrend !== null) {
        result.waveTrendWt1.push([x, waveTrend.wt1]);
        result.waveTrendWt2.push([x, waveTrend.wt2]);
        if (waveTrend.vwap) {
          result.waveTrendVwap.push([x, waveTrend.vwap]);
        }
        if (waveTrend.crossesOver) {
          result.waveTrendBuy.push([x, waveTrend.wt1]);
        }
        if (waveTrend.crossesUnder) {
          result.waveTrendSell.push([x, waveTrend.wt1]);
        }
      }

      if (mfi !== null) {
        if (mfi.mfi > 0) {
          result.mfiBuy.push([x, mfi.mfi]);
        } else {
          result.mfiSell.push([x, mfi.mfi]);
        }
      }
    }
  });

  return result;
}
