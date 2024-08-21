import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { TopBar } from "../TopBar";

vi.mock("react-router-dom", async () => {
  const actual = await vi.importActual("react-router-dom");
  return {
    ...actual,
    useNavigate: vi.fn(),
  };
});
describe("TopBar component tests", () => {
  it("renders TopBar component", () => {
    // Arrange && Act
    render(
      <TestingProvider>
        <TopBar />
      </TestingProvider>,
    );
    // Assert
    expect(screen.getByLabelText("charts")).toBeInTheDocument();
    expect(screen.queryByText("Simple Charts")).not.toBeVisible();
    expect(screen.queryByText("Advanced Charts")).not.toBeVisible();
  });

  it("opens menu and shows menu items on icon click", () => {
    // Arrange
    render(
      <TestingProvider>
        <TopBar />
      </TestingProvider>,
    );
    // Act
    fireEvent.click(screen.getByLabelText("charts"));
    // Assert
    expect(screen.getByText("Simple Charts")).toBeVisible();
    expect(screen.getByText("Advanced Charts")).toBeVisible();
  });
});
