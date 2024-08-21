import { render, screen } from "@testing-library/react";
import { RouterProps, TestingProvider } from "../../../__fixtures__/TestingProvider";
import { navigationRoutes } from "../../../consts/navigationRoutes";
import { routerConfig } from "../routerConfig";

vi.mock("../../../views/SimpleChartsView", () => ({
  SimpleChartsView: () => <div data-testid={"SimpleChartsViewTestId"} />,
}));
vi.mock("../../../views/AdvancedChartsView", () => ({
  AdvancedChartsView: () => <div data-testid={"AdvancedChartsViewTestId"} />,
}));

describe("router config tests", () => {
  test("render router - SimpleCharts path - returns simpleChartsContainer", async () => {
    // Arrange
    const router: RouterProps = {
      routes: routerConfig,
      initialEntries: [`/${navigationRoutes.SimpleCharts}`],
    };
    // Act
    render(<TestingProvider overrideRouter={router} />);

    // Assert
    await screen.findByTestId("SimpleChartsViewTestId");
    expect(screen.getByTestId("SimpleChartsViewTestId")).toBeInTheDocument();
  });

  test("render router - AdvancedCharts path - returns AdvancedChartsView", async () => {
    // Arrange
    const router: RouterProps = {
      routes: routerConfig,
      initialEntries: [`/${navigationRoutes.AdvancedCharts}`],
    };
    // Act
    render(<TestingProvider overrideRouter={router} />);

    // Assert
    await screen.findByTestId("AdvancedChartsViewTestId");
    expect(screen.getByTestId("AdvancedChartsViewTestId")).toBeInTheDocument();
  });
});
