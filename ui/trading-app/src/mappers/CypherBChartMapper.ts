import { ApexCypherBChartData } from "../types/ApexCypherBChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToApexChartData(
  quotes: CypherBQuote[]
): ApexCypherBChartData {
  const result: ApexCypherBChartData = {
    momentumWave: [],
    mfi: [],
    vwap: [],
    lowestY: Infinity,
    highestY: -Infinity,
  };

  for (let i = 0; i < quotes.length; i++) {
    const timestamp = Date.parse(quotes[i].ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quotes[i].ohlc.date);
      const { momentumWave, mfi, vwap } = quotes[i];

      result.momentumWave.push({ y: momentumWave, x });
      result.mfi.push({ y: mfi, x });
      result.vwap.push({ y: vwap, x });

      result.lowestY = Math.min(result.lowestY, momentumWave, mfi, vwap);
      result.highestY = Math.max(result.highestY, momentumWave, mfi, vwap);
    }
  }
  return result;
}