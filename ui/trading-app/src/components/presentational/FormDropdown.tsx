import { FormControl, InputLabel } from "@mui/material";
import { Control, Controller } from "react-hook-form";
import { IChartSettingsPanelForm } from "../forms/ChartSettingsPanelForm";
import { Dropdown, Option } from "./Dropdown";

export interface FormDropdownProps {
  options: Option[];
  name: keyof IChartSettingsPanelForm;
  label: string;
  control: Control<IChartSettingsPanelForm>;
}

export const FormDropdown = ({ options, name, label, control }: FormDropdownProps) => (
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
      name={name}
    />
  </FormControl>
);
