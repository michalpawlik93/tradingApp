import { Controller, Control } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { IChartSettingsPanelForm } from "../../components/forms/ChartSettingsPanelForm";
import { FormControl } from "@mui/material";
export interface FormDateTimePickerProps {
  name: keyof IChartSettingsPanelForm;
  label: string;
  control: Control<IChartSettingsPanelForm>;
  minDate: Date;
  maxDate: Date;
}

export const FormDateTimePicker = ({ name, control, label }: FormDateTimePickerProps) => {
  return (
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
};
