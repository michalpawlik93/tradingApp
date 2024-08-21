import { render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { Layout } from "../Layout";

vi.mock("react-router-dom", async () => {
  const actual = await vi.importActual("react-router-dom");
  return {
    ...actual,
    useNavigate: vi.fn(),
  };
});
describe("Layout component tests", () => {
  it("renders Layout component", () => {
    // Arrange && Act
    render(
      <TestingProvider>
        <Layout />
      </TestingProvider>,
    );
    // Assert
    expect(screen.getByLabelText("charts")).toBeInTheDocument();
  });
});
