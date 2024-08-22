import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { mockUseCypherBQuotes } from "../../__fixtures__/useCypherBQuotesMock";
import { mockUseTimeFrameHook } from "../../__fixtures__/useTimeFrameHookMock";
import { AdvancedChartsView } from "../AdvancedChartsView";

describe("AdvancedChartsView tests", () => {
  test("Charts are rendered", () => {
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
