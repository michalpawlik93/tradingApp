import { describe, expect } from "vitest";
import { CypherBQuote } from "../../types/CypherBQuote";
import { mapToCypherBChartData } from "../CypherBChartDataMapper";
import { CypherBQuoteMock, MfiMock, QuoteMock, WaveTrendMock } from "../../__fixtures__/quotes";
import { CypherBChartData } from "../../types/ChartData";
describe("mapToCypherBChartData", () => {
  test("should map quotes to wave trend chart data", () => {
    const quotes: CypherBQuote[] = [CypherBQuoteMock()];
    const quote = QuoteMock();
    const waveTrendMock = WaveTrendMock();
    const expected: CypherBChartData = {
      waveTrendWt1: [[Date.parse(quote.date), waveTrendMock.wt1]],
      waveTrendWt2: [[Date.parse(quote.date), waveTrendMock.wt2]],
      waveTrendVwap: [[Date.parse(quote.date), waveTrendMock.vwap as number]],
      waveTrendSell: [[Date.parse(quote.date), waveTrendMock.wt1]],
      waveTrendBuy: [[Date.parse(quote.date), waveTrendMock.wt1]],
      mfiSell: [],
      mfiBuy: [[Date.parse(quote.date), MfiMock().mfi]],
      ohlc: [[Date.parse(quote.date), quote.open, quote.close, quote.low, quote.high]],
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
        waveTrend: WaveTrendMock(),
        mfi: MfiMock(),
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
    };

    const result = mapToCypherBChartData(quotes);
    expect(result).toEqual(expected);
  });
});
