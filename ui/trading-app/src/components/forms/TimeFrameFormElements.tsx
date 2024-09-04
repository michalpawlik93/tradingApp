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

export const timeFrameFormDefaultValues = (): ITimeFrameFormValues => ({
  startDate: new Date(Date.UTC(2023, 5, 26)),
  endDate: new Date(Date.UTC(2023, 5, 28)),
  granularity: Granularity.FiveMins,
});

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

interface TimeFrameFormElementsProps<T extends ITimeFrameFormValues> extends MaxMinDate {
  control: Control<T>;
}
