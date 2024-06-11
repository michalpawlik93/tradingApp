import { render, screen } from "@testing-library/react";
import { SimpleChartsView } from "../SimpleChartsView";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { mockUseCombinedQuotes } from "../../__fixtures__/useCombinedQuotesMock";

vi.mock("react-apexcharts", () => ({ __esModule: true, default: () => <div>Chart</div> }));
describe("SimpleChartsView tests", () => {
  test("Charts are rendered", async () => {
    // Arrange
    mockUseCombinedQuotes();
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
