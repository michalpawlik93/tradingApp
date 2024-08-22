import { render, screen } from "@testing-library/react";
import { useForm } from "react-hook-form";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { IChartSettingsPanelForm } from "../../forms/ChartSettingsPanelForm";
import { FormDateTimePicker, FormDateTimePickerProps } from "../FormDateTimePicker";

export interface ITimeFrameFormValuesMock {
  startDate: Date;
}
describe("FormDateTimePicker component tests", () => {
  const renderFormDateTimePicker = (
    props: Partial<FormDateTimePickerProps<ITimeFrameFormValuesMock>> = {},
  ) => {
    const defaultProps: FormDateTimePickerProps<ITimeFrameFormValuesMock> = {
      name: "startDate",
      label: "Start Date",
      control: {} as any,
      minDate: new Date("2020-01-01"),
      maxDate: new Date("2025-01-01"),
    };

    const mergedProps = { ...defaultProps, ...props };

    const TestForm = () => {
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
});
