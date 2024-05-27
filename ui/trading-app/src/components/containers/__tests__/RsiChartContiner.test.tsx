import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { RsiChartContiner } from "../RsiChartContiner";

vi.mock("react-apexcharts", () => ({ __esModule: true, default: () => <div>RsiChart</div> }));
describe("RsiChartContiner tests", () => {
  test("RsiChart is rendered", async () => {
    // Arrange
    // Act
    render(
      <TestingProvider>
        <RsiChartContiner combinedQuotes={[]} />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getByText("RsiChart")).toBeInTheDocument();
  });
});
