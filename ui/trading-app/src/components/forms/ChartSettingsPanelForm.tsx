import { Option } from "../presentational/Dropdown";
import { Granularity } from "../../consts/granularity";
import { AssetName } from "../../consts/assetName";
import { AssetType } from "../../consts/assetType";
import { Box, Typography, Paper } from "@mui/material";
import { css } from "@emotion/react";
import { useForm } from "react-hook-form";
import { CommonButton } from "../presentational/Button";
import { FormDropdown } from "../presentational/FormDropdown";
import { FormDateTimePicker } from "../presentational/FormDateTimePicker";
import { useQuotesStore } from "../../stores/quotesStore";
import {
  mfiSettingsDefault,
  sRsiSettingsDefault,
  waveTrendSettingsDefault,
} from "../../consts/technicalIndicatorsSettings";

export interface IChartSettingsPanelForm {
  startDate: Date;
  endDate: Date;
  granularity: string;
  assetName: string;
  assetType: string;
}

export const defaultValues: IChartSettingsPanelForm = {
  startDate: new Date(2023, 5, 24),
  endDate: new Date(2023, 5, 28),
  granularity: Granularity.Hourly,
  assetName: AssetName.USDPLN,
  assetType: AssetType.Currencies,
};

const chartSettingsPanelCss = {
  container: () =>
    css({
      padding: "1rem",
      backgroundColor: "#f9f9f9",
      borderRadius: "8px",
      boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
    }),
  header: () =>
    css({
      marginBottom: "1rem",
    }),
  form: () =>
    css({
      display: "grid",
      gridTemplateColumns: "repeat(auto-fit, minmax(200px, 1fr))",
      gap: "1rem",
      alignItems: "center",
    }),
  buttonBar: () =>
    css({
      marginTop: "1rem",
      display: "flex",
      justifyContent: "flex-end",
      gap: "1rem",
    }),
};

export interface ChartSettingsPanelFormProps {
  minDate: Date;
  maxDate: Date;
}

export const ChartSettingsPanelForm = ({ minDate, maxDate }: ChartSettingsPanelFormProps) => {
  const fetchData = useQuotesStore((state) => state.fetchCypherBQuotes);
  const { handleSubmit, reset, control } = useForm<IChartSettingsPanelForm>({
    defaultValues: defaultValues,
  });

  const onSubmit = async (data: IChartSettingsPanelForm) => {
    console.log(data);
    await fetchData({
      asset: {
        name: data.assetName,
        type: data.assetType,
      },
      timeFrame: {
        granularity: data.granularity,
        startDate: data.startDate.toISOString(),
        endDate: data.endDate.toISOString(),
      },
      waveTrendSettings: waveTrendSettingsDefault,
      sRsiSettings: sRsiSettingsDefault,
      mfiSettings: mfiSettingsDefault,
    });
  };

  return (
    <Paper css={chartSettingsPanelCss.container}>
      <Typography variant="h6" css={chartSettingsPanelCss.header}>
        Chart Settings
      </Typography>
      <Box component="form" onSubmit={handleSubmit(onSubmit)} css={chartSettingsPanelCss.form}>
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
        <FormDateTimePicker
          name="startDate"
          control={control}
          label="Start Date"
          minDate={minDate ?? undefined}
          maxDate={maxDate ?? undefined}
        />
        <FormDateTimePicker
          name="endDate"
          control={control}
          label="End Date"
          minDate={minDate ?? undefined}
          maxDate={maxDate ?? undefined}
        />
        <Box css={chartSettingsPanelCss.buttonBar}>
          <CommonButton text="Submit" type="submit" size="small" />
          <CommonButton text="Reset" type="reset" secondary size="small" onClick={() => reset()} />
        </Box>
      </Box>
    </Paper>
  );
};
