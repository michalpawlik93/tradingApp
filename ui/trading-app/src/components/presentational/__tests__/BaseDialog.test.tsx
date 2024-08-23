import { fireEvent, render, screen } from "@testing-library/react";
import { describe, expect, test, vi } from "vitest";
import { BaseDialog } from "../BaseDialog";

describe("BaseDialog Component", () => {
  const onClose = vi.fn();

  const title = "Test Title";
  const message = "Test Message";

  test("Should render correctly", () => {
    // Arrange

    // Act
    render(
      <BaseDialog title={title} open onClose={onClose}>
        <div>Dialog Content</div>
      </BaseDialog>,
    );

    // Assert
    expect(screen.getByText(title)).toBeInTheDocument();
    expect(screen.getByText("Dialog Content")).toBeInTheDocument();
  });

  test("Should render with close button", () => {
    // Arrange

    // Act
    render(
      <BaseDialog title={title} open onClose={onClose} closeButton>
        <div>Dialog Content</div>
      </BaseDialog>,
    );

    // Assert
    const iconButton = screen.getByTestId("close-icon-button");
    const closeIcon = screen.getByTestId("close-icon");

    expect(iconButton).toBeInTheDocument();
    expect(closeIcon).toBeInTheDocument();
  });

  test("Should close dialog when close icon is clicked", () => {
    render(
      <BaseDialog title={title} open onClose={onClose} closeButton>
        <div>Dialog Content</div>
      </BaseDialog>,
    );

    const closeIcon = screen.getByTestId("close-icon");

    fireEvent.click(closeIcon);

    expect(onClose).toHaveBeenCalledTimes(1);
  });

  test("Should render with message without close button", () => {
    render(<BaseDialog title={title} open onClose={onClose} message={message} />);

    expect(screen.queryByTestId("close-icon-button")).not.toBeInTheDocument();
    expect(screen.getByText(message)).toBeInTheDocument();
  });
});
