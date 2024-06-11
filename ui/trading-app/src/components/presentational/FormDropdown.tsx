import { Controller, Control } from "react-hook-form";
import { IChartSettingsPanelForm } from "../forms/ChartSettingsPanelForm";
import { Dropdown, Option } from "./Dropdown";
import { FormControl, InputLabel } from "@mui/material";

export interface FormDropdownProps {
  options: Option[];
  name: keyof IChartSettingsPanelForm;
  label: string;
  control: Control<IChartSettingsPanelForm>;
}

export const FormDropdown: React.FC<FormDropdownProps> = ({ options, name, label, control }) => {
  return (
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
};
