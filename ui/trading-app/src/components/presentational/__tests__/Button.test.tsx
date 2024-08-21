import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, it, vi } from "vitest";
import { CommonButton } from "../Button";

describe("CommonButton component tests", () => {
  it("renders a button with given text", () => {
    // Arrange
    const buttonText = "Click me";

    // Act
    render(<CommonButton text={buttonText} />);

    // Assert
    expect(screen.getByText(buttonText)).toBeInTheDocument();
  });

  it("renders a disabled button when disabled prop is true", () => {
    // Arrange
    const buttonText = "Click me";

    // Act
    render(<CommonButton text={buttonText} disabled />);

    // Assert
    const button = screen.getByText(buttonText);
    expect(button).toBeDisabled();
  });

  it("calls onClick handler when button is clicked", () => {
    // Arrange
    const buttonText = "Click me";
    const onClick = vi.fn();

    // Act
    render(<CommonButton text={buttonText} onClick={onClick} />);
    const button = screen.getByText(buttonText);
    fireEvent.click(button);

    // Assert
    expect(onClick).toHaveBeenCalledTimes(1);
  });

  it("renders a secondary button when secondary prop is true", () => {
    // Arrange
    const buttonText = "Click me";

    // Act
    render(<CommonButton text={buttonText} secondary />);

    // Assert
    const button = screen.getByText(buttonText);
    expect(button).toHaveClass("MuiButton-outlined");
  });

  it("renders a button with startIcon when startIcon prop is provided", () => {
    // Arrange
    const buttonText = "Click me";
    const startIcon = <span>🚀</span>;

    // Act
    render(<CommonButton text={buttonText} startIcon={startIcon} />);

    // Assert
    const button = screen.getByText(buttonText);
    expect(button).toContainElement(screen.getByText("🚀"));
  });

  // it("renders a button with specified size", () => {
  //   // Arrange
  //   const buttonText = "Click me";

  //   // Act
  //   render(<CommonButton text={buttonText} size="extra-small" />);

  //   // Assert
  //   const button = screen.getByText(buttonText);
  //   expect(button).toHaveClass("MuiButton-sizeExtraSmall");
  // });

  it("renders a button with specified type", () => {
    // Arrange
    const buttonText = "Click me";

    // Act
    render(<CommonButton text={buttonText} type="submit" />);

    // Assert
    const button = screen.getByText(buttonText);
    expect(button).toHaveAttribute("type", "submit");
  });
});
