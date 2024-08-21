import React from "react";
import { fireEvent, render, renderHook, screen } from "@testing-library/react";
import { Control, useForm } from "react-hook-form";
import { Granularity } from "../../../consts/granularity";
import { IChartSettingsPanelForm } from "../../forms/ChartSettingsPanelForm";
import { Option } from "../Dropdown";
import { FormDropdown } from "../FormDropdown";

const options: Option[] = Object.values(Granularity).map((x): Option => [x, x]);

const TestComponent = ({ control }: { control: Control<IChartSettingsPanelForm> }) => (
  <FormDropdown options={options} label="Test Dropdown" control={control} name={"granularity"} />
);

describe("FormDropdown tests", () => {
  it("renders correctly with given options", () => {
    // Arrange
    const { result } = renderHook(() => useForm<IChartSettingsPanelForm>());

    // Act
    render(<TestComponent control={result.current.control} />);

    expect(screen.getAllByText("Test Dropdown")[0]).toBeInTheDocument();
    const selectElement = screen.getByRole("combobox");
    fireEvent.mouseDown(selectElement);

    // Assert
    options.forEach(([_optionValue, optionLabel]) => {
      expect(screen.getByText(optionLabel)).toBeInTheDocument();
    });
  });
});
