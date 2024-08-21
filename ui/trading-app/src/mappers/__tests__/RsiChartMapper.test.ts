import { describe, expect } from "vitest";
import { CombinedQuoteMock } from "../../__fixtures__/quotes";
import { rsiSettingsDefault } from "../../consts/technicalIndicatorsSettings";
import { RsiChartData } from "../../types/ChartData";
import { mapToRsiChartData } from "../RsiChartMapper";

describe("mapToApexRsiChartData", () => {
  test("should return empty array", () => {
    // Arrange

    // Act
    const result = mapToRsiChartData([], rsiSettingsDefault);

    // Assert
    expect(result.overbought).toBe(rsiSettingsDefault.overbought);
    expect(result.oversold).toBe(rsiSettingsDefault.oversold);
    expect(result.rsi).toHaveLength(0);
  });

  test("should map to rsi chart data", () => {
    // Arrange
    const data = CombinedQuoteMock();
    const expected: RsiChartData = {
      rsi: [[Date.parse(data.ohlc.date), data.rsi]],
      overbought: rsiSettingsDefault.overbought,
      oversold: rsiSettingsDefault.oversold,
    };
    // Act
    const result = mapToRsiChartData([data], rsiSettingsDefault);

    // Assert
    expect(result.rsi).toHaveLength(1);
    expect(result).toEqual(expected);
  });
});
