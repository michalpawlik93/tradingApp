import { renderHook, waitFor } from "@testing-library/react";
import { createQuotesDataServiceMock } from "../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { QuotesDataService } from "../../services/QuotesDataService";
import { useCypherBQuotes } from "../useCypherBQuotes";

vi.unmock("../useCypherBQuotes");

describe("useCypherBQuotes tests", () => {
  test("useCypherBQuotes - should fetch cypherb quotes", async () => {
    // Arrange
    vi.mocked(QuotesDataService.getCypherB).mockImplementation(
      createQuotesDataServiceMock().getCypherB,
    );
    // Act
    const { result } = renderHook(useCypherBQuotes, {
      wrapper: TestingProvider,
    });

    // Assert
    await waitFor(() => {
      expect(result.current.cypherBQuotes).toHaveLength(1);
    });
  });
});
