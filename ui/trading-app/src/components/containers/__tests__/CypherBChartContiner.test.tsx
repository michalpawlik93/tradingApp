import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { CypherBChartContiner } from "../CypherBChartContiner";
import { mockUseCypherBQuotes } from "../../../__fixtures__/useCypherBQuotesMock";
import { mockUseTimeFrameHook } from "../../../__fixtures__/useTimeFrameHookMock";

vi.mock("react-apexcharts", () => ({ __esModule: true, default: () => <div>Chart</div> }));
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
    expect(screen.getAllByText("Chart")).toHaveLength(2);
  });
});
