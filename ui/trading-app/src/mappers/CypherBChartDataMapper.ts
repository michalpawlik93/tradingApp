import { TradeAction } from "../consts/tradeAction";
import { CypherBChartData } from "../types/ChartData";
import { CypherBQuote } from "../types/CypherBQuote";

export function mapToCypherBChartData(quotes: CypherBQuote[]): CypherBChartData {
  const result = initializeCypherBChartData();

  quotes.forEach((quote) => {
    const timestamp = Date.parse(quote.ohlc.date);
    if (!Number.isNaN(timestamp)) {
      const x = new Date(quote.ohlc.date).getTime();

      mapOhlc(result, x, quote);
      mapWaveTrendSignals(result, x, quote);
      mapSrsiSignals(result, x, quote);
      mapMfiResults(result, x, quote);
    }
  });

  return result;
}

const initializeCypherBChartData = (): CypherBChartData => ({
  waveTrendWt1: [],
  waveTrendWt2: [],
  waveTrendVwap: [],
  waveTrendSell: [],
  waveTrendBuy: [],
  mfiBuy: [],
  mfiSell: [],
  ohlc: [],
  srsiStochD: [],
  srsiStochK: [],
  srsiBuy: [],
  srsiSell: [],
});

function mapOhlc(result: CypherBChartData, x: number, quote: CypherBQuote) {
  result.ohlc.push([x, quote.ohlc.open, quote.ohlc.close, quote.ohlc.low, quote.ohlc.high]);
}

function mapWaveTrendSignals(result: CypherBChartData, x: number, quote: CypherBQuote) {
  const { waveTrendSignal } = quote;

  if (waveTrendSignal !== null) {
    result.waveTrendWt1.push([x, waveTrendSignal.wt1]);
    result.waveTrendWt2.push([x, waveTrendSignal.wt2]);

    if (waveTrendSignal.vwap) {
      result.waveTrendVwap.push([x, waveTrendSignal.vwap]);
    }

    if (waveTrendSignal.tradeAction === TradeAction.Buy) {
      result.waveTrendBuy.push([x, waveTrendSignal.wt1]);
    }

    if (waveTrendSignal.tradeAction === TradeAction.Sell) {
      result.waveTrendSell.push([x, waveTrendSignal.wt1]);
    }
  }
}

function mapSrsiSignals(result: CypherBChartData, x: number, quote: CypherBQuote) {
  const { srsiSignal } = quote;

  if (srsiSignal !== null) {
    result.srsiStochK.push([x, srsiSignal.stochK]);
    result.srsiStochD.push([x, srsiSignal.stochD]);

    if (srsiSignal.tradeAction === TradeAction.Buy) {
      result.srsiBuy.push([x, srsiSignal.stochK]);
    }

    if (srsiSignal.tradeAction === TradeAction.Sell) {
      result.srsiSell.push([x, srsiSignal.stochK]);
    }
  }
}

function mapMfiResults(result: CypherBChartData, x: number, quote: CypherBQuote) {
  const { mfiResult } = quote;

  if (mfiResult !== null) {
    if (mfiResult.mfi > 0) {
      result.mfiBuy.push([x, mfiResult.mfi]);
    } else {
      result.mfiSell.push([x, mfiResult.mfi]);
    }
  }
}
