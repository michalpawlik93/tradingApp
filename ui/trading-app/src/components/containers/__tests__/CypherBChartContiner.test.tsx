import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { mockUseCypherBQuotes } from "../../../__fixtures__/useCypherBQuotesMock";
import { mockUseTimeFrameHook } from "../../../__fixtures__/useTimeFrameHookMock";
import { CypherBChartContiner } from "../CypherBChartContiner";

describe("CypherBChartContiner tests", () => {
  test("Charts are rendered", () => {
    // Arrange
    mockUseCypherBQuotes();
    mockUseTimeFrameHook();
    // Act
    render(
      <TestingProvider>
        <CypherBChartContiner />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getAllByTestId("echarts-react")).toHaveLength(1);
  });
});
