import { FormControl } from "@mui/material";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { Control, Controller, FieldPath, FieldValues } from "react-hook-form";
import { MaxMinDate } from "../../types/MaxMinDate";

export interface FormDateTimePickerProps<T extends FieldValues> extends MaxMinDate {
  name: string;
  label: string;
  control: Control<T>;
}

export const FormDateTimePicker = <T extends FieldValues>({
  name,
  control,
  label,
  minDate,
  maxDate,
}: FormDateTimePickerProps<T>) => (
  <FormControl size="small">
    <Controller
      name={name as FieldPath<T>}
      control={control}
      render={({ field }) => {
        const handleDatePickerChange = (date: Date | null) => {
          field.onChange(date);
        };

        return (
          <DateTimePicker
            label={label}
            value={field.value as Date | null}
            onChange={handleDatePickerChange}
            minDate={minDate}
            maxDate={maxDate}
          />
        );
      }}
    />
  </FormControl>
);
