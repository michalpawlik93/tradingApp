import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { SimpleChartsContainer } from "../SimpleChartsContainer";
import { mockUseCombinedQuotes } from "../../../__fixtures__/useCombinedQuotesMock";

describe("SimpleChartsContainer tests", () => {
  test("Charts are rendered", async () => {
    // Arrange
    mockUseCombinedQuotes();
    // Act
    render(
      <TestingProvider>
        <SimpleChartsContainer />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getAllByTestId("echarts-react")).toHaveLength(2);
  });
});
