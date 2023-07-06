import { Option } from "../presentational/Dropdown";
import { Granularity } from "../../consts/granularity";
import { AssetName } from "../../consts/assetName";
import { AssetType } from "../../consts/assetType";
import { Box } from "@mui/material";
import { css } from "@emotion/react";
import { useForm } from "react-hook-form";
import { CommonButton } from "../presentational/Button";
import { FormDropdown } from "../presentational/FormDropdown";
import { FormDateTimePicker } from "../presentational/FormDateTimePicker";

export interface IFormInput {
  startDate: Date;
  endDate: Date;
  granularity: string;
  assetType: string;
  assetName: string;
}

const defaultValues: IFormInput = {
  startDate: new Date(),
  endDate: new Date(),
  granularity: Granularity.FiveMins,
  assetType: AssetType.Cryptocurrency,
  assetName: AssetName.ANC,
};

const chartSettingsPanelCss = {
  chartSettingsPanelStyle: () =>
    css({
      display: "flex",
      alignItems: "center",
      gap: "3.25rem",
      marginBottom: "0.5rem",
    }),
};

export interface ChartSettingsPanelFormProps {
  minDate: Date;
  maxDate: Date;
}

export const ChartSettingsPanelForm = ({
  minDate,
  maxDate,
}: ChartSettingsPanelFormProps) => {
  const { handleSubmit, reset, control } = useForm<IFormInput>({
    defaultValues: defaultValues,
  });
  const onSubmit = (data: IFormInput) => console.log(data);
  return (
    <Box css={chartSettingsPanelCss.chartSettingsPanelStyle}>
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
      <FormDropdown
        name="granularity"
        control={control}
        label="Granularity"
        options={Object.values(Granularity).map((x): Option => [x, x])}
      />
      <FormDropdown
        name="assetName"
        control={control}
        label="Asset Name"
        options={Object.values(AssetName).map((x): Option => [x, x])}
      />
      <FormDropdown
        name="assetType"
        control={control}
        label="Asset Type"
        options={Object.values(AssetType).map((x): Option => [x, x])}
      />
      <CommonButton text="Submit" onClick={handleSubmit(onSubmit)} />
      <CommonButton text="Reset" onClick={() => reset()} />
    </Box>
  );
};
