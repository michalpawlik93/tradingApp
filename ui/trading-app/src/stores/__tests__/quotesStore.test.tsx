import { renderHook, waitFor } from "@testing-library/react";
import { useQuotesStore } from "../quotesStore";
import { rsiSettingsDefault } from "src/consts/technicalIndicatorsSettings";
import { createQuotesDataServiceMock } from "src/__fixtures__/QuotesDataServiceMock";
import { QuotesDataService } from "../../services/QuotesDataService";
import { GetCypherBDtoMock, GetQuotesRequestDtoMock } from "src/__fixtures__/quotes";

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
    result.current.fetchCombinedQuotes(GetQuotesRequestDtoMock());
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
    result.current.fetchCypherBQuotes(GetCypherBDtoMock());
    //Assert
    await waitFor(() => {
      expect(result.current.cypherBQuotes).toHaveLength(1);
    });
  });
});
