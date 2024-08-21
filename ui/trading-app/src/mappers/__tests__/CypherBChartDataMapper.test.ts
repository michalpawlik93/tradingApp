import { describe, expect } from "vitest";
import {
  CypherBQuoteMock,
  MfiMock,
  QuoteMock,
  SrsiMock,
  WaveTrendMock,
} from "../../__fixtures__/quotes";
import { TradeAction } from "../../consts/tradeAction";
import { CypherBChartData } from "../../types/ChartData";
import { CypherBQuote } from "../../types/CypherBQuote";
import { mapToCypherBChartData } from "../CypherBChartDataMapper";

describe("mapToCypherBChartData", () => {
  test("should map quotes to wave trend chart data for Buy trade action", () => {
    const quotes: CypherBQuote[] = [CypherBQuoteMock()];
    const quote = QuoteMock();
    const waveTrendMock = WaveTrendMock();
    const srsiMock = SrsiMock();
    const expected: CypherBChartData = {
      waveTrendWt1: [[Date.parse(quote.date), waveTrendMock.wt1]],
      waveTrendWt2: [[Date.parse(quote.date), waveTrendMock.wt2]],
      waveTrendVwap: [[Date.parse(quote.date), waveTrendMock.vwap as number]],
      waveTrendSell: [],
      waveTrendBuy: [[Date.parse(quote.date), waveTrendMock.wt1]],
      mfiSell: [],
      mfiBuy: [[Date.parse(quote.date), MfiMock().mfi]],
      ohlc: [[Date.parse(quote.date), quote.open, quote.close, quote.low, quote.high]],
      srsiStochD: [[Date.parse(quote.date), srsiMock.stochD]],
      srsiStochK: [[Date.parse(quote.date), srsiMock.stochK]],
      srsiBuy: [[Date.parse(quote.date), srsiMock.stochK]],
      srsiSell: [],
    };

    const result = mapToCypherBChartData(quotes);
    expect(result).toEqual(expected);
  });

  test("should map quotes to wave trend chart data for Sell trade action", () => {
    const quotes: CypherBQuote[] = [
      CypherBQuoteMock({
        ohlc: QuoteMock(),
        waveTrendSignal: { ...WaveTrendMock(), tradeAction: TradeAction.Sell },
        mfiResult: { ...MfiMock(), mfi: -20 },
        srsiSignal: { ...SrsiMock(), tradeAction: TradeAction.Sell },
      }),
    ];
    const quote = QuoteMock();
    const waveTrendMock = WaveTrendMock();
    const srsiMock = SrsiMock();
    const expected: CypherBChartData = {
      waveTrendWt1: [[Date.parse(quote.date), waveTrendMock.wt1]],
      waveTrendWt2: [[Date.parse(quote.date), waveTrendMock.wt2]],
      waveTrendVwap: [[Date.parse(quote.date), waveTrendMock.vwap as number]],
      waveTrendSell: [[Date.parse(quote.date), waveTrendMock.wt1]],
      waveTrendBuy: [],
      mfiSell: [[Date.parse(quote.date), -20]],
      mfiBuy: [],
      ohlc: [[Date.parse(quote.date), quote.open, quote.close, quote.low, quote.high]],
      srsiStochD: [[Date.parse(quote.date), srsiMock.stochD]],
      srsiStochK: [[Date.parse(quote.date), srsiMock.stochK]],
      srsiBuy: [],
      srsiSell: [[Date.parse(quote.date), srsiMock.stochK]],
    };

    const result = mapToCypherBChartData(quotes);
    expect(result).toEqual(expected);
  });

  test("should handle empty quotes array", () => {
    const quotes: CypherBQuote[] = [];
    const expected: CypherBChartData = {
      waveTrendWt1: [],
      waveTrendWt2: [],
      waveTrendVwap: [],
      waveTrendSell: [],
      waveTrendBuy: [],
      mfiBuy: [],
      mfiSell: [],
      ohlc: [],
      srsiBuy: [],
      srsiSell: [],
      srsiStochD: [],
      srsiStochK: [],
    };

    const result = mapToCypherBChartData(quotes);
    expect(result).toEqual(expected);
  });

  test("should ignore invalid dates", () => {
    const quotes: CypherBQuote[] = [
      {
        ohlc: {
          ...QuoteMock(),
          date: "invalid-date",
        },
        waveTrendSignal: WaveTrendMock(),
        mfiResult: MfiMock(),
        srsiSignal: SrsiMock(),
      },
    ];

    const expected: CypherBChartData = {
      waveTrendWt1: [],
      waveTrendWt2: [],
      waveTrendVwap: [],
      waveTrendSell: [],
      waveTrendBuy: [],
      mfiBuy: [],
      mfiSell: [],
      ohlc: [],
      srsiBuy: [],
      srsiSell: [],
      srsiStochD: [],
      srsiStochK: [],
    };

    const result = mapToCypherBChartData(quotes);
    expect(result).toEqual(expected);
  });
});
