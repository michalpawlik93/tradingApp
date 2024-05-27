import { render } from "@testing-library/react";
import { describe, expect } from "vitest";
import { Breadcrumbs } from "../Breadcrumb";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";

describe("Breadcrumbs tests", () => {
  test("renders breadcrumbs based on route matches", () => {
    //Arrange && Act
    const { getByText } = render(
      <TestingProvider>
        <Breadcrumbs />
      </TestingProvider>,
    );
    // Assert
    expect(getByText("Test Router")).toBeInTheDocument();
  });
});
