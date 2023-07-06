import { FC } from "react";
import MenuItem from "@mui/material/MenuItem";
import Select, { SelectChangeEvent } from "@mui/material/Select";

export interface DropdownProps {
  options: Option[];
  value: string;
  label: string;
  onChange: (value: string) => void;
}

export type Option = [number | string, string];

export const Dropdown: FC<DropdownProps> = ({
  options,
  value,
  label,
  onChange,
}) => {
  const handleChange = (event: SelectChangeEvent<string>) => {
    onChange(event.target.value);
  };

  return (
    <Select value={value} label={label} onChange={handleChange}>
      {options.map(([optionValue, optionLabel]) => (
        <MenuItem key={optionValue} value={optionValue}>
          {optionLabel}
        </MenuItem>
      ))}
    </Select>
  );
};
