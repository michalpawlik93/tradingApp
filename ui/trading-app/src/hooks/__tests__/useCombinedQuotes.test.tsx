import { renderHook } from "@testing-library/react";
import { useCombinedQuotes } from "../useCombinedQuotes";
import { TestingProvider } from "../../__fixtures__/TestingProvider";

describe("useCombinedQuotes", () => {
  test("should return empty arrays", () => {
    // Arrange

    // Act
    const { result } = renderHook(useCombinedQuotes, {
      wrapper: TestingProvider,
    });

    // Assert
    expect(result.current.combinedQuotes).toHaveLength(0);
  });
});
