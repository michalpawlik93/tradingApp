import { Controller, Control } from "react-hook-form";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { IChartSettingsPanelForm } from "../../components/forms/ChartSettingsPanelForm";
import { FormControl, InputLabel } from "@mui/material";
import { styled } from "@mui/system";

const StyledInputLabel = styled(InputLabel)`
  position: absolute;
  top: -0.75rem;
  left: 0.75rem;
  padding: 0 0.25rem;
`;

export interface FormDateTimePickerProps {
  name: keyof IChartSettingsPanelForm;
  label: string;
  control: Control<IChartSettingsPanelForm>;
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
              value={field.value as Date}
              onChange={handleDatePickerChange}
            />
          );
        }}
      />
    </FormControl>
  );
};
