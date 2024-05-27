import React from "react";
import { render, screen } from "@testing-library/react";
import { describe, it, expect, vi } from "vitest";
import { Dropdown, Option } from "../Dropdown";

describe("Dropdown component tests", () => {
  test("Should render disabled input", () => {
    // Arrange
    const options: Option[] = [
      [1, "Yes"],
      [2, "No"],
    ];
    const onChange = vi.fn();
    const value = "1";

    // Act
    render(
      <Dropdown options={options} label="test" value={value} onChange={onChange} disabled={true} />,
    );

    // Assert
    const selectElement = screen.getByRole("combobox");
    expect(selectElement).toHaveAttribute("aria-disabled");
  });
});
