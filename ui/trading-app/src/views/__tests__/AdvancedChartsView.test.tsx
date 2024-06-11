import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { AdvancedChartsView } from "../AdvancedChartsView";
import { mockUseCypherBQuotes } from "../../__fixtures__/useCypherBQuotesMock";
import { mockUseTimeFrameHook } from "../../__fixtures__/useTimeFrameHookMock";

vi.mock("react-apexcharts", () => ({ __esModule: true, default: () => <div>Chart</div> }));
describe("AdvancedChartsView tests", () => {
  test("Charts are rendered", async () => {
    // Arrange
    mockUseCypherBQuotes();
    mockUseTimeFrameHook();
    // Act
    render(
      <TestingProvider>
        <AdvancedChartsView />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getByText("Advanced Charts")).toBeInTheDocument();
  });
});
