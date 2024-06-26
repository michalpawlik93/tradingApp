import { renderHook } from "@testing-library/react";
import { useCombinedQuotes } from "../useCombinedQuotes";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { waitFor } from "@testing-library/react";
import { QuotesDataService } from "../../services/QuotesDataService";
import { createQuotesDataServiceMock } from "../../__fixtures__/QuotesDataServiceMock";

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
