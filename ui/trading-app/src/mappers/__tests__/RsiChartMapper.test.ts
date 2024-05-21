import { expect, it, describe } from "vitest";
import { mapToApexRsiChartData } from "../RsiChartMapper";
import { RsiSettings } from "../../types/RsiSettings";

describe("mapToApexRsiChartData", () => {
  it("should return empty arrays", () => {
    // Arrange
    const rsiSettings: RsiSettings = { overbought: 1, oversold: 2 };

    // Act
    const result = mapToApexRsiChartData([], rsiSettings);

    // Assert
    expect(result.overbought).toHaveLength(0);
    expect(result.oversold).toHaveLength(0);
    expect(result.rsi).toHaveLength(0);
  });
});
