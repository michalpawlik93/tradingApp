import { render, screen } from "@testing-library/react";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { RsiChartContiner } from "../RsiChartContiner";

describe("RsiChartContiner tests", () => {
  test("RsiChart is rendered", () => {
    // Arrange
    // Act
    render(
      <TestingProvider>
        <RsiChartContiner combinedQuotes={[]} />
      </TestingProvider>,
    );

    // Assert
    expect(screen.getByTestId("echarts-react")).toBeInTheDocument();
  });
});
