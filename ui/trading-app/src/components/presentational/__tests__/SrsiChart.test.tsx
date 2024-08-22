import { render, screen } from "@testing-library/react";
import { getInstanceByDom } from "echarts";
import { SrsiQuoteMock } from "../../../__fixtures__/quotes";
import { SrsiChart } from "../Charts/SrsiChart";

describe("SrsiChart", () => {
  test("should render the SrsiChart component", () => {
    // Arrange
    // Act
    render(<SrsiChart quotes={[SrsiQuoteMock()]} />);
    // Assert
    expect(screen.getByTestId("echarts-react")).toBeInTheDocument();
    expect(getInstanceByDom).toHaveBeenCalledTimes(1);
  });
});
