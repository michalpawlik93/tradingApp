import { TradeAction } from "../consts/tradeAction";
import { SrsiStandaloneChartData } from "../types/ChartData";
import { SrsiQuote } from "../types/SrsiQuote";

export function mapToSrsiChartData(quotes: SrsiQuote[]): SrsiStandaloneChartData {
  const srsiStochK: (number | Date)[][] = [];
  const srsiStochD: (number | Date)[][] = [];
  const srsiSell: (number | Date)[][] = [];
  const srsiBuy: (number | Date)[][] = [];
  const close: (number | Date)[][] = [];

  const closeValues = quotes.map((quote) => quote.ohlc.open);
  const minClose = Math.min(...closeValues);
  const maxClose = Math.max(...closeValues);

  const scaleClose = (value: number) => ((value - minClose) / (maxClose - minClose)) * 100;

  quotes.forEach((quote) => {
    const date = new Date(quote.ohlc.date);
    const scaledClose = scaleClose(quote.ohlc.open);
    close.push([date, scaledClose]);

    if (quote.srsiSignal) {
      srsiStochK.push([date, quote.srsiSignal.stochK]);
      srsiStochD.push([date, quote.srsiSignal.stochD]);

      if (quote.srsiSignal.tradeAction === TradeAction.Buy) {
        srsiBuy.push([date, quote.srsiSignal.stochK]);
      } else if (quote.srsiSignal.tradeAction === TradeAction.Sell) {
        srsiSell.push([date, quote.srsiSignal.stochK]);
      }
    }
  });

  return {
    srsiStochK,
    srsiStochD,
    srsiSell,
    srsiBuy,
    close,
  };
}
