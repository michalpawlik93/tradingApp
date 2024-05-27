import React from "react";
import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { FormDateTimePicker, FormDateTimePickerProps } from "../FormDateTimePicker";
import { useForm } from "react-hook-form";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { IChartSettingsPanelForm } from "../../forms/ChartSettingsPanelForm";

describe("FormDateTimePicker component tests", () => {
  const renderFormDateTimePicker = (props: Partial<FormDateTimePickerProps> = {}) => {
    const defaultProps: FormDateTimePickerProps = {
      name: "startDate",
      label: "Start Date",
      control: {} as any, // To be overridden by the actual form control
      minDate: new Date("2020-01-01"),
      maxDate: new Date("2025-01-01"),
    };

    const mergedProps = { ...defaultProps, ...props };

    const TestForm: React.FC = () => {
      const { control } = useForm<IChartSettingsPanelForm>({
        defaultValues: {
          startDate: new Date("2021-01-01"),
        },
      });

      return (
        <TestingProvider>
          <FormDateTimePicker {...mergedProps} control={control} />
        </TestingProvider>
      );
    };

    render(<TestForm />);
  };

  test("should render the DateTimePicker with the correct label", () => {
    renderFormDateTimePicker();
    expect(screen.getByText("Start Date")).toBeInTheDocument();
  });

  test("should display the default date value", () => {
    renderFormDateTimePicker();
    const inputElement = screen.getByRole("textbox");
    expect(inputElement).toHaveValue("01/01/2021 01:00 AM");
  });
});
