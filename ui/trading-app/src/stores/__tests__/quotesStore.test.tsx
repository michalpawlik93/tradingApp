import { renderHook, waitFor } from "@testing-library/react";
import { useQuotesStore } from "../quotesStore";
import { rsiSettingsDefault } from "src/consts/technicalIndicatorsSettings";
import { createQuotesDataServiceMock } from "src/__fixtures__/QuotesDataServiceMock";
import { QuotesDataService } from "../../services/QuotesDataService";

vi.unmock("../quotesStore");

describe("useQuotesStore", () => {
  test("default state - intial values are set correctly", () => {
    //Arrange
    //Act
    const { result } = renderHook(() => useQuotesStore());
    //Assert
    expect(result.current.combinedQuotes).toHaveLength(0);
    expect(result.current.cypherBQuotes).toHaveLength(0);
    expect(result.current.rsiSettings).toEqual(rsiSettingsDefault);
  });

  test("fetchCombinedQuotes - request - combinedQuotes fetched", async () => {
    //Arrange
    vi.mocked(QuotesDataService.getCombinedQuotes).mockImplementation(
      createQuotesDataServiceMock().getCombinedQuotes,
    );
    //Act
    const { result } = renderHook(() => useQuotesStore());
    result.current.fetchCombinedQuotes({
      technicalIndicators: [],
      granularity: "",
      assetType: "",
      assetName: "",
    });
    //Assert
    await waitFor(() => {
      expect(result.current.combinedQuotes).toHaveLength(1);
    });
  });

  test("fetchCypherBQuotes - request - cypherBQuotes fetched", async () => {
    //Arrange
    vi.mocked(QuotesDataService.getCypherB).mockImplementation(
      createQuotesDataServiceMock().getCypherB,
    );
    //Act
    const { result } = renderHook(() => useQuotesStore());
    result.current.fetchCypherBQuotes({
      asset: {
        name: "",
        type: "",
      },
      timeFrame: {
        granularity: "",
      },
      waveTrendSettings: {
        channelLength: 0,
        averageLength: 0,
        movingAverageLength: 0,
        oversold: 0,
        overbought: 0,
      },
      sRsiSettings: {
        enable: false,
        length: 0,
        stochKSmooth: 0,
        stochDSmooth: 0,
      },
    });
    //Assert
    await waitFor(() => {
      expect(result.current.cypherBQuotes).toHaveLength(1);
    });
  });
});
