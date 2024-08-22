import { FormControl, InputLabel } from "@mui/material";
import { Control, Controller, FieldPath, FieldValues } from "react-hook-form";
import { Dropdown, Option } from "./Dropdown";

export interface FormDropdownProps<T extends FieldValues> {
  options: Option[];
  name: string;
  label: string;
  control: Control<T>;
}

export const FormDropdown = <T extends FieldValues>({
  options,
  name,
  label,
  control,
}: FormDropdownProps<T>) => (
  <FormControl size="small">
    <InputLabel>{label}</InputLabel>
    <Controller
      render={({ field }) => (
        <Dropdown
          options={options}
          label={label}
          onChange={field.onChange}
          value={field.value as string}
        />
      )}
      control={control}
      name={name as FieldPath<T>}
    />
  </FormControl>
);
