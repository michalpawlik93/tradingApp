import { describe, expect } from "vitest";
import { QuoteMock } from "../../__fixtures__/quotes";
import { OhlcChartData } from "../../types/ChartData";
import { mapToOhlcChartData } from "../OhlcChartDataMapper";

describe("OhlcChartDataMapper", () => {
  test("should map to rsi chart data", () => {
    // Arrange
    const data = QuoteMock();
    const expected: OhlcChartData = {
      ohlc: [[Date.parse(data.date), data.open, data.close, data.low, data.high]],
    };
    // Act
    const result = mapToOhlcChartData([data]);

    // Assert
    expect(result.ohlc).toHaveLength(1);
    expect(result).toEqual(expected);
  });
});
