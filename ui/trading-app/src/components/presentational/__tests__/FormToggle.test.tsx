import React from "react";
import { fireEvent, render, renderHook, screen } from "@testing-library/react";
import { Control, useForm } from "react-hook-form";
import { FormToggle } from "../../presentational/FormToggle";

interface FormValues {
  enableFeature: boolean;
}

const WrapperComponent = ({ control }: { control: Control<FormValues> }) => (
  <FormToggle<FormValues> name="enableFeature" control={control} label="Enable Feature" />
);

describe("FormToggle", () => {
  test("renders correctly with the initial value", () => {
    //Arrange
    const { result } = renderHook(() =>
      useForm<FormValues>({
        defaultValues: {
          enableFeature: false,
        },
      }),
    );

    //Act
    render(<WrapperComponent control={result.current.control} />);

    //Assert
    expect(screen.getByText("Enable Feature")).toBeInTheDocument();

    const toggle = screen.getByRole("checkbox");
    expect(toggle).toBeInTheDocument();
    expect(toggle).not.toBeChecked();
  });

  test("toggles the value on click", () => {
    //Arrange
    const { result } = renderHook(() =>
      useForm<FormValues>({
        defaultValues: {
          enableFeature: false,
        },
      }),
    );

    //Act
    render(<WrapperComponent control={result.current.control} />);

    //Assert
    const toggle = screen.getByRole("checkbox");

    expect(toggle).not.toBeChecked();

    fireEvent.click(toggle);
    expect(toggle).toBeChecked();

    fireEvent.click(toggle);
    expect(toggle).not.toBeChecked();
  });

  test("respects the initial value from defaultValues", () => {
    //Arrange
    const { result } = renderHook(() =>
      useForm<FormValues>({
        defaultValues: {
          enableFeature: true,
        },
      }),
    );

    //Act
    render(<WrapperComponent control={result.current.control} />);

    //Assert
    const toggle = screen.getByRole("checkbox");
    expect(toggle).toBeChecked();
  });
});
