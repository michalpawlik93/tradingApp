import { renderHook, waitFor } from "@testing-library/react";
import { createQuotesDataServiceMock } from "../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { QuotesDataService } from "../../services/QuotesDataService";
import { useCombinedQuotes } from "../useCombinedQuotes";

vi.unmock("../useCombinedQuotes");

describe("useCombinedQuotes tests", () => {
  test("useCombinedQuotes - should fetch combined quotes", async () => {
    // Arrange
    vi.mocked(QuotesDataService.getCombinedQuotes).mockImplementation(
      createQuotesDataServiceMock().getCombinedQuotes,
    );
    // Act
    const { result } = renderHook(useCombinedQuotes, {
      wrapper: TestingProvider,
    });

    // Assert
    await waitFor(() => {
      expect(result.current.combinedQuotes).toHaveLength(1);
    });
  });
});
