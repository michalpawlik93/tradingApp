import { Control } from "react-hook-form";
import { Granularity } from "../../consts/granularity";
import { MaxMinDate } from "../../types/MaxMinDate";
import { Option } from "../presentational/Dropdown";
import { FormDateTimePicker } from "../presentational/FormDateTimePicker";
import { FormDropdown } from "../presentational/FormDropdown";

export interface ITimeFrameFormValues {
  granularity: string;
  startDate: Date;
  endDate: Date;
}

interface TimeFrameFormElementsProps<T extends ITimeFrameFormValues> extends MaxMinDate {
  control: Control<T>;
}

export const TimeFrameFormElements = <T extends ITimeFrameFormValues>({
  control,
  minDate,
  maxDate,
}: TimeFrameFormElementsProps<T>) => (
  <>
    <FormDropdown<T>
      name="granularity"
      control={control}
      label="Granularity"
      options={Object.values(Granularity).map((x): Option => [x, x])}
    />
    <FormDateTimePicker
      name="startDate"
      control={control}
      label="Start Date"
      minDate={minDate}
      maxDate={maxDate}
    />
    <FormDateTimePicker
      name="endDate"
      control={control}
      label="End Date"
      minDate={minDate}
      maxDate={maxDate}
    />
  </>
);
