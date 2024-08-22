import { FormControl, FormControlLabel, Switch } from "@mui/material";
import { Control, Controller, FieldPath, FieldValues } from "react-hook-form";

export interface FormToggleProps<T extends FieldValues> {
  name: string;
  label: string;
  control: Control<T>;
}

export const FormToggle = <T extends FieldValues>({ name, control, label }: FormToggleProps<T>) => (
  <FormControl size="small">
    <Controller
      name={name as FieldPath<T>}
      control={control}
      render={({ field }) => (
        <FormControlLabel
          labelPlacement="top"
          control={
            <Switch
              {...field}
              checked={field.value || false}
              onChange={(e) => field.onChange(e.target.checked)}
            />
          }
          label={label}
        />
      )}
    />
  </FormControl>
);
