import { FormControl } from "@mui/material";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { Control, Controller } from "react-hook-form";
import { IChartSettingsPanelForm } from "../../components/forms/ChartSettingsPanelForm";

export interface FormDateTimePickerProps {
  name: keyof IChartSettingsPanelForm;
  label: string;
  control: Control<IChartSettingsPanelForm>;
  minDate: Date;
  maxDate: Date;
}

export const FormDateTimePicker = ({ name, control, label }: FormDateTimePickerProps) => (
  <FormControl size="small">
    <Controller
      name={name}
      control={control}
      render={({ field }) => {
        const handleDatePickerChange = (date: Date | null) => {
          field.onChange(date as any);
        };

        return (
          <DateTimePicker
            label={label}
            value={field.value as Date}
            onChange={handleDatePickerChange}
          />
        );
      }}
    />
  </FormControl>
);
