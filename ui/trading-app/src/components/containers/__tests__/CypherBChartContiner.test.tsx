import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { CypherBChartContiner } from "../CypherBChartContiner";
import { mockUseCypherBQuotes } from "../../../__fixtures__/useCypherBQuotesMock";
import { mockUseTimeFrameHook } from "../../../__fixtures__/useTimeFrameHookMock";

describe("CypherBChartContiner tests", () => {
  test("Charts are rendered", async () => {
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
