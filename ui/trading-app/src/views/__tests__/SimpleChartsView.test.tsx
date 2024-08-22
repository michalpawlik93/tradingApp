import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { mockUseCombinedQuotes } from "../../__fixtures__/useCombinedQuotesMock";
import { mockUseTimeFrameHook } from "../../__fixtures__/useTimeFrameHookMock";
import { SimpleChartsView } from "../SimpleChartsView";

describe("SimpleChartsView tests", () => {
  test("Charts are rendered", () => {
    // Arrange
    mockUseCombinedQuotes();
    mockUseTimeFrameHook();
    // Act
    render(
      <TestingProvider>
        <SimpleChartsView />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getByText("Simple Charts")).toBeInTheDocument();
  });
});
