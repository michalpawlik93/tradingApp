import { render, screen } from "@testing-library/react";
import { describe, expect, it } from "vitest";
import { ButtonBar } from "../ButtonBar";

describe("ButtonBar component tests", () => {
  it("renders a button group with children", () => {
    // Arrange
    const buttonText = "Click me";

    // Act
    render(
      <ButtonBar>
        <button>{buttonText}</button>
      </ButtonBar>,
    );

    // Assert
    expect(screen.getByText(buttonText)).toBeInTheDocument();
  });

  it("renders a button group with multiple children", () => {
    // Arrange
    const buttonText1 = "Button 1";
    const buttonText2 = "Button 2";

    // Act
    render(
      <ButtonBar>
        <button>{buttonText1}</button>
        <button>{buttonText2}</button>
      </ButtonBar>,
    );

    // Assert
    expect(screen.getByText(buttonText1)).toBeInTheDocument();
    expect(screen.getByText(buttonText2)).toBeInTheDocument();
  });

  it("renders a button group with outlined variant", () => {
    // Arrange
    const buttonText = "Click me";

    // Act
    render(
      <ButtonBar>
        <button>{buttonText}</button>
      </ButtonBar>,
    );

    // Assert
    const buttonGroup = screen.getByLabelText("outlined button group");
    expect(buttonGroup).toHaveClass("MuiButtonGroup-outlined");
  });
});
