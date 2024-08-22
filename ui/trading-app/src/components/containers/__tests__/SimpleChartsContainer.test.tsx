import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { mockUseCombinedQuotes } from "../../../__fixtures__/useCombinedQuotesMock";
import { SimpleChartsContainer } from "../SimpleChartsContainer";

describe("SimpleChartsContainer tests", () => {
  test("Charts are rendered", () => {
    // Arrange
    mockUseCombinedQuotes();
    // Act
    render(
      <TestingProvider>
        <SimpleChartsContainer />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getAllByTestId("echarts-react")).toHaveLength(3);
  });
});
