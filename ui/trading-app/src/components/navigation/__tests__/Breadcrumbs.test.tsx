import { render, screen } from "@testing-library/react";
import { describe, expect } from "vitest";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { Breadcrumbs } from "../Breadcrumb";

describe("Breadcrumbs tests", () => {
  test("renders breadcrumbs based on route matches", () => {
    //Arrange && Act
    render(
      <TestingProvider>
        <Breadcrumbs />
      </TestingProvider>,
    );
    // Assert
    expect(screen.getByText("Test Router")).toBeInTheDocument();
  });
});
