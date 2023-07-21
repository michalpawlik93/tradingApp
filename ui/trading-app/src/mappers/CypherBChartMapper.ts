import { ApexCypherBChartData } from "../types/ApexCypherBChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToApexChartData(
  quotes: CypherBQuote[]
): ApexCypherBChartData {
  const result: ApexCypherBChartData = {
    waveTrend: [],
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

      result.waveTrend.push({
        y: waveTrend.value,
        x,
        crossesOver: waveTrend.crossesOver,
        crossesUnder: waveTrend.crossesUnder,
      });
      result.mfi.push({ y: mfi, x });
      result.vwap.push({ y: vwap, x });

      result.lowestY = Math.min(result.lowestY, waveTrend.value, mfi, vwap);
      result.highestY = Math.max(result.highestY, waveTrend.value, mfi, vwap);
    }
  }
  return result;
}
