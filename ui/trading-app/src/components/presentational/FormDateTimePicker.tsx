import { Controller, Control } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { GetQuotesDtoRequest } from "../../services/dtos/GetQuotesDtoRequest";
import { FormControl, InputLabel } from "@mui/material";
import { styled } from "@mui/system";

const StyledInputLabel = styled(InputLabel)`
  position: absolute;
  top: -0.75rem;
  left: 0.75rem;
  padding: 0 0.25rem;
`;

export interface FormDateTimePickerProps {
  name: keyof GetQuotesDtoRequest;
  label: string;
  control: Control<GetQuotesDtoRequest>;
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
      <StyledInputLabel>{label}</StyledInputLabel>
      <Controller
        name={name}
        control={control}
        render={({ field }) => {
          const handleDatePickerChange = (date: Date | null) => {
            field.onChange(date as any);
          };

          return (
            <DateTimePicker
              value={field.value ? new Date(field.value) : null}
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
