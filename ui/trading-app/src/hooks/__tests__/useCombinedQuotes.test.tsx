import { renderHook, waitFor } from "@testing-library/react";
import { GetCombinedQuotesRequestDtoMock } from "../../__fixtures__/quotes";
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
      initialProps: GetCombinedQuotesRequestDtoMock(),
      wrapper: TestingProvider,
    });

    // Assert
    await waitFor(() => {
      expect(result.current.rsiQuotes).toHaveLength(1);
    });
    await waitFor(() => {
      expect(result.current.srsiQuotes).toHaveLength(1);
    });
  });
});
