import { ApexCypherBChartData } from "../types/ApexCypherBChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToApexChartData(quotes: CypherBQuote[]): ApexCypherBChartData {
  const result: ApexCypherBChartData = {
    waveTrendWt1: [],
    waveTrendWt2: [],
    waveTrendVwap: [],
    mfi: [],
    vwap: [],
    lowestY: Infinity,
    highestY: -Infinity,
  };

  for (let i = 0; i < quotes.length; i++) {
    const timestamp = Date.parse(quotes[i].ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quotes[i].ohlc.date);
      const { waveTrend, mfi, vwap } = quotes[i];

      if (waveTrend !== null) {
        result.waveTrendWt1.push({
          y: waveTrend.wt1,
          x,
          crossesOver: waveTrend.crossesOver,
          crossesUnder: waveTrend.crossesUnder,
        });
        result.waveTrendWt2.push({
          y: waveTrend.wt2,
          x,
        });
        if (waveTrend.vwap !== null) {
          result.waveTrendVwap.push({
            y: waveTrend.vwap as number,
            x,
          });
        }
      }
      result.mfi.push({ y: mfi, x });
      result.vwap.push({ y: vwap, x });

      // result.lowestY = Math.min(
      //   result.lowestY,
      //   waveTrend.wt1,
      //   waveTrend.wt2,
      //   waveTrend.vwap,
      //   mfi,
      //   vwap,
      // );
      // result.highestY = Math.max(
      //   result.highestY,
      //   waveTrend.wt1,
      //   waveTrend.wt2,
      //   waveTrend.vwap,
      //   mfi,
      //   vwap,
      // );
    }
  }
  return result;
}
