import { render, screen } from "@testing-library/react";
import ErrorFallback from "../ErrorFallback";

describe("Root component tests", () => {
  test("Error Boundary - catches error thrown by child component", () => {
    // Arrange
    const error = new Error("Test error");

    // Act
    render(<ErrorFallback resetErrorBoundary={() => null} error={error} />);

    // Assert
    expect(screen.getByText("Something went wrong:")).toBeInTheDocument();
  });
});
