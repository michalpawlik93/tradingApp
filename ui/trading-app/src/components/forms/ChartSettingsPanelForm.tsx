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
import { useQuotesStore } from "../../stores/quotesStore";
import ButtonGroup from "@mui/material/ButtonGroup";
import { GetQuotesRequestDto } from "../../services/dtos/GetQuotesRequestDto";
import {
  sRsiSettingsDefault,
  waveTrendSettingsDefault,
} from "../../consts/technicalIndicatorsSettings";

export interface IChartSettingsPanelForm
  extends Pick<GetQuotesRequestDto, "granularity" | "assetType" | "assetName"> {
  startDate?: Date;
  endDate?: Date;
}

export const defaultValues: IChartSettingsPanelForm = {
  startDate: new Date(),
  endDate: new Date(),
  granularity: Granularity.Hourly,
  assetType: AssetType.Cryptocurrency,
  assetName: AssetName.BTC,
};

const chartSettingsPanelCss = {
  chartSettingsPanelStyle: () =>
    css({
      display: "flex",
      alignItems: "center",
      gap: "3.25rem",
      marginBottom: "0.5rem",
    }),
  buttonBar: () =>
    css({
      marginTop: "auto",
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
      },
      waveTrendSettings: waveTrendSettingsDefault,
      sRsiSettings: sRsiSettingsDefault,
    });
  };
  return (
    <Box css={chartSettingsPanelCss.chartSettingsPanelStyle}>
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
      <ButtonGroup css={chartSettingsPanelCss.buttonBar}>
        <CommonButton text="Submit" type="submit" onClick={handleSubmit(onSubmit)} />
        <CommonButton text="Reset" type="reset" secondary onClick={() => reset()} />
      </ButtonGroup>
    </Box>
  );
};
