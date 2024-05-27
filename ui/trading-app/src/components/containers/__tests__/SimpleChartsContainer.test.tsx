import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { SimpleChartsContainer } from "../SimpleChartsContainer";
import { mockUseCombinedQuotes } from "../../../__fixtures__/useCombinedQuotesMock";

vi.mock("react-apexcharts", () => ({ __esModule: true, default: () => <div>Chart</div> }));
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
    expect(screen.getAllByText("Chart")).toHaveLength(2);
  });
});
