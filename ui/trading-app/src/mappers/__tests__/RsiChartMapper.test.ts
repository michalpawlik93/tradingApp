import { expect, it, describe } from "vitest";
import { mapToApexRsiChartData } from "../RsiChartMapper";
import { RsiSettings } from "../../types/RsiSettings";

describe("mapToApexRsiChartData", () => {
  it("should return empty arrays", () => {
    const rsiSettings: RsiSettings = { overbought: 1, oversold: 2 };

    const result = mapToApexRsiChartData([], rsiSettings);

    expect(result.overbought).toHaveLength(0);
    expect(result.oversold).toHaveLength(0);
    expect(result.rsi).toHaveLength(0);
  });
});
