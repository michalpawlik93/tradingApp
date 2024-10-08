import MenuItem from "@mui/material/MenuItem";
import Select, { SelectChangeEvent } from "@mui/material/Select";

export interface DropdownProps {
  options: Option[];
  value: string;
  label: string;
  onChange: (value: string) => void;
  disabled?: boolean;
}

export type Option = [number | string, string];

export const Dropdown = ({ options, value, label, onChange, disabled = false }: DropdownProps) => {
  const handleChange = (event: SelectChangeEvent<string>) => {
    onChange(event.target.value);
  };

  return (
    <Select value={value} label={label} onChange={handleChange} disabled={disabled}>
      {options.map(([optionValue, optionLabel]) => (
        <MenuItem key={optionValue} value={optionValue}>
          {optionLabel}
        </MenuItem>
      ))}
    </Select>
  );
};
