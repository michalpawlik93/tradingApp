import { renderHook, waitFor } from "@testing-library/react";
import { GetCombinedQuotesRequestDtoMock, GetCypherBDtoMock } from "../../__fixtures__/quotes";
import { createQuotesDataServiceMock } from "../../__fixtures__/QuotesDataServiceMock";
import { rsiSettingsDefault } from "../../consts/technicalIndicatorsSettings";
import { QuotesDataService } from "../../services/QuotesDataService";
import { useQuotesStore } from "../quotesStore";

describe("useQuotesStore", () => {
  test("default state - intial values are set correctly", () => {
    //Arrange
    //Act
    const { result } = renderHook(() => useQuotesStore());
    //Assert
    expect(result.current.rsiQuotes).toHaveLength(0);
    expect(result.current.srsiQuotes).toHaveLength(0);
    expect(result.current.rsiSettings).toEqual(rsiSettingsDefault);
  });

  test("fetchCombinedQuotes - request - all combinedQuotes fetched", async () => {
    //Arrange
    vi.mocked(QuotesDataService.getCombinedQuotes).mockImplementation(
      createQuotesDataServiceMock().getCombinedQuotes,
    );
    //Act
    const { result } = renderHook(() => useQuotesStore());
    result.current.fetchCombinedQuotes(GetCombinedQuotesRequestDtoMock());
    //Assert
    await waitFor(() => {
      expect(result.current.rsiQuotes).toHaveLength(1);
      expect(result.current.srsiQuotes).toHaveLength(1);
      expect(result.current.rsiSettings).not.toBeNull();
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
