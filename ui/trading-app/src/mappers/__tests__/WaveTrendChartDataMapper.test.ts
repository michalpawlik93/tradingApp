import { describe, it, expect } from "vitest";
import { CypherBQuote } from "../../types/CypherBQuote";
import { mapToWaveTrendChartData } from "../WaveTrendChartDataMapper";
import { WaveTrendChartData } from "../../types/WaveTrendChartData";
import { CypherBQuoteMock, QuoteMock, WaveTrendMock } from "../../__fixtures__/quotes";
describe("mapToWaveTrendChartData", () => {
  test("should map quotes to wave trend chart data", () => {
    const quotes: CypherBQuote[] = [CypherBQuoteMock()];
    const quote = QuoteMock();
    const waveTrendWt1Value = 12.1314;
    const expected: WaveTrendChartData = {
      waveTrendWt1: [[Date.parse(quote.date), waveTrendWt1Value]],
      waveTrendWt2: [[Date.parse(quote.date), 13.1314]],
      sellSignals: [[Date.parse(quote.date), waveTrendWt1Value]],
      buySignals: [[Date.parse(quote.date), waveTrendWt1Value]],
    };

    const result = mapToWaveTrendChartData(quotes);
    expect(result).toEqual(expected);
  });

  test("should handle empty quotes array", () => {
    const quotes: CypherBQuote[] = [];
    const expected: WaveTrendChartData = {
      waveTrendWt1: [],
      waveTrendWt2: [],
      sellSignals: [],
      buySignals: [],
    };

    const result = mapToWaveTrendChartData(quotes);
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
        mfi: 50,
        vwap: 30471.3006,
      },
    ];

    const expected: WaveTrendChartData = {
      waveTrendWt1: [],
      waveTrendWt2: [],
      sellSignals: [],
      buySignals: [],
    };

    const result = mapToWaveTrendChartData(quotes);
    expect(result).toEqual(expected);
  });
});
