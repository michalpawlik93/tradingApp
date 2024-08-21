import { TradeAction } from "../consts/tradeAction";
import { SrsiChartData } from "../types/ChartData";
import { SrsiQuote } from "../types/SrsiQuote";

export function mapToSrsiChartData(quotes: SrsiQuote[]): SrsiChartData {
  const srsiStochK: (number | Date)[][] = [];
  const srsiStochD: (number | Date)[][] = [];
  const srsiSell: (number | Date)[][] = [];
  const srsiBuy: (number | Date)[][] = [];

  quotes.forEach((quote) => {
    const date = new Date(quote.ohlc.date);

    srsiStochK.push([date, quote.srsi.stochK]);
    srsiStochD.push([date, quote.srsi.stochD]);

    if (quote.srsi.tradeAction === TradeAction.Buy) {
      srsiBuy.push([date, quote.srsi.stochK]);
    } else if (quote.srsi.tradeAction === TradeAction.Sell) {
      srsiSell.push([date, quote.srsi.stochK]);
    }
  });

  return {
    srsiStochK,
    srsiStochD,
    srsiSell,
    srsiBuy,
  };
}
