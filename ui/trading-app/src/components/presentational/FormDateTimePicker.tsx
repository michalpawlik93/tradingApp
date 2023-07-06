import { Controller, Control } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { IFormInput } from "../forms/ChartSettingsPanelForm";
import { FormControl, InputLabel } from "@mui/material";

export interface FormDateTimePickerProps {
  name: keyof IFormInput;
  label: string;
  control: Control<IFormInput>;
  minDate: Date;
  maxDate: Date;
}

export const FormDateTimePicker = ({
  name,
  control,
  label,
  minDate,
  maxDate,
}: FormDateTimePickerProps) => {
  return (
    <FormControl size="small">
      <InputLabel>{label}</InputLabel>
      <Controller
        name={name}
        control={control}
        render={({ field }) => {
          const handleDatePickerChange = (date: Date | null) => {
            field.onChange(date as any);
          };

          return (
            <DateTimePicker
              value={field.value as Date | null}
              onChange={handleDatePickerChange}
              minDateTime={minDate}
              maxDateTime={maxDate}
            />
          );
        }}
      />
    </FormControl>
  );
};
