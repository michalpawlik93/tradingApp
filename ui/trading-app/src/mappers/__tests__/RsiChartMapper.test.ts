import { expect, it, describe } from "vitest";
import { mapToRsiChartData } from "../RsiChartMapper";
import { rsiSettingsDefault } from "../../consts/technicalIndicatorsSettings";
import { CombinedQuoteMock } from "../../__fixtures__/quotes";
import { RsiChartData } from "../../types/ChartData";

describe("mapToApexRsiChartData", () => {
  it("should return empty array", () => {
    // Arrange

    // Act
    const result = mapToRsiChartData([], rsiSettingsDefault);

    // Assert
    expect(result.overbought).toBe(rsiSettingsDefault.overbought);
    expect(result.oversold).toBe(rsiSettingsDefault.oversold);
    expect(result.rsi).toHaveLength(0);
  });

  it("should map to rsi chart data", () => {
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
