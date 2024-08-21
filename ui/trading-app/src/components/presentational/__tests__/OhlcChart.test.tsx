import { render, screen } from "@testing-library/react";
import { getInstanceByDom } from "echarts";
import { QuoteMock } from "../../../__fixtures__/quotes";
import { OhlcChart } from "../Charts/OhlcChart";

describe("OhlcChart", () => {
  test("should render the OhlcChart component", () => {
    // Arrange
    // Act
    render(<OhlcChart quotes={[QuoteMock()]} />);
    // Assert
    expect(screen.getByTestId("echarts-react")).toBeInTheDocument();
    expect(getInstanceByDom).toHaveBeenCalledTimes(1);
  });
});
