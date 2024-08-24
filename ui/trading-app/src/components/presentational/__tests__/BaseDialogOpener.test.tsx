import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, test, vi } from "vitest";
import { BaseDialogOpener } from "../BaseDialogOpener";

vi.mock("./Button", () => ({
  CommonButton: ({ text, onClick }: { text: string; onClick: () => void }) => (
    <button onClick={onClick}>{text}</button>
  ),
}));

describe("BaseDialogOpener Component", () => {
  test("Sholud render button", () => {
    const buttonText = "Button Test Text";

    render(<BaseDialogOpener buttonText={buttonText} />);

    const button = screen.getByText(buttonText);
    expect(button).toBeInTheDocument();
  });

  test("Sholud render button with text and modal with children, message and title", () => {
    const buttonText = "Button Test Text";
    const dialogTitle = "Dialog Title";
    const message = "Test Message";

    render(
      <BaseDialogOpener buttonText={buttonText} message={message} dialogTitle={dialogTitle}>
        <div>Dialog content</div>
      </BaseDialogOpener>,
    );

    const button = screen.getByText(buttonText);
    expect(button).toBeInTheDocument();

    fireEvent.click(button);

    const dialog = screen.getByTestId("base-dialog-component");
    expect(dialog).toBeInTheDocument();
    expect(screen.getByText(message)).toBeInTheDocument();
    expect(screen.getByText("Dialog content")).toBeInTheDocument();
  });

  test("Sholud render button with text and modal with close button", () => {
    const buttonText = "Button Test Text";
    const dialogTitle = "Dialog Title";
    const message = "Test Message";

    render(
      <BaseDialogOpener
        buttonText={buttonText}
        message={message}
        dialogTitle={dialogTitle}
        closeButton
      >
        <div>Dialog content</div>
      </BaseDialogOpener>,
    );

    const button = screen.getByText(buttonText);
    expect(button).toBeInTheDocument();

    fireEvent.click(button);

    const iconButton = screen.getByTestId("close-icon-button");
    const closeIcon = screen.getByTestId("close-icon");

    expect(iconButton).toBeInTheDocument();
    expect(closeIcon).toBeInTheDocument();
  });
});
