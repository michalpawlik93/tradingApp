import { CypherBChart } from "../Charts/CypherBChart";
import { CypherBQuoteMock } from "../../../__fixtures__/quotes";
import { render, screen } from "@testing-library/react";
import { getInstanceByDom } from "echarts";

describe("CypherBChart", () => {
  test("should render the CypherBChart component", () => {
    // Arrange
    // Act
    render(<CypherBChart quotes={[CypherBQuoteMock()]} />);
    // Assert
    expect(screen.getByTestId("echarts-react")).toBeInTheDocument();
    expect(getInstanceByDom).toHaveBeenCalledTimes(1);
  });
});
