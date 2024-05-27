import { render, screen } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import { Layout } from "../Layout";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";

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
