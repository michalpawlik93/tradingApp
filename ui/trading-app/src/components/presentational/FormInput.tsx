import { FormControl, TextField } from "@mui/material";
import { Control, Controller, FieldPath, FieldValues } from "react-hook-form";

export interface FormInputProps<T extends FieldValues> {
  name: string;
  label: string;
  control: Control<T>;
  type?: "text" | "number";
}

export const FormInput = <T extends FieldValues>({
  name,
  control,
  label,
  type = "text",
}: FormInputProps<T>) => (
  <FormControl size="small">
    <Controller
      name={name as FieldPath<T>}
      control={control}
      render={({ field }) => (
        <TextField
          {...field}
          id={name as string}
          label={label}
          type={type}
          onChange={(e) => field.onChange(e.target.value)}
          value={field.value || ""}
          InputProps={{ inputMode: type === "number" ? "numeric" : "text" }}
          InputLabelProps={{ shrink: true }}
        />
      )}
    />
  </FormControl>
);
