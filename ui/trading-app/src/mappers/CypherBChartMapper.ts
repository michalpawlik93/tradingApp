import { ApexCypherBChartData } from "../types/ApexCypherBChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToApexChartData(
  quotes: CypherBQuote[]
): ApexCypherBChartData {
  const result: ApexCypherBChartData = {
    momentumWave: [],
    mfi: [],
    vwap: [],
  };
  for (let i = 0; i < quotes.length; i++) {
    const timestamp = Date.parse(quotes[i].ohlc.date);
    if (!isNaN(timestamp)) {
      const x = new Date(quotes[i].ohlc.date);
      result.momentumWave.push({ y: quotes[i].momentumWave, x: x });
      result.mfi.push({ y: quotes[i].mfi, x: x });
      result.vwap.push({ y: quotes[i].vwap, x: x });
    }
  }
  return result;
}
