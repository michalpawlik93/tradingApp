import { OhlcChartData } from "../components/presentational/Charts/OhlcChart";
import { Quote } from "../types/Quote";

export function mapToOhlcChartData(quotes: Quote[]): OhlcChartData {
  const categoryData = quotes.map((quote) => quote.date);
  const values = quotes.map((quote) => [quote.open, quote.close, quote.low, quote.high]);

  return { categoryData, values };
}
