import { css } from "@emotion/react";
import { Box, Paper, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { TechnicalIndicators } from "../../consts/technicalIndicators";
import { useQuotesStore } from "../../stores/quotesStore";
import { MaxMinDate } from "../../types/MaxMinDate";
import { CommonButton } from "../presentational/Button";
import { assetFormDefaultValues, AssetFormElements, IAssetFormValues } from "./AssetFormElements";
import {
  ISrsiSettingsFormValues,
  srsiSettingsFormDefaultValues,
  SrsiSettingsFormElements,
} from "./SrsiSettingsFormElements";
import {
  ITimeFrameFormValues,
  timeFrameFormDefaultValues,
  TimeFrameFormElements,
} from "./TimeFrameFormElements";
import {
  ITradingStreategyValues,
  tradingStreategyDefaultValues,
  TradingStreategyFormElement,
} from "./TradingStreategyFormElement";

export interface ISrsiChartForm
  extends ITimeFrameFormValues,
    IAssetFormValues,
    ITradingStreategyValues,
    ISrsiSettingsFormValues {}

export const defaultValues: ISrsiChartForm = {
  ...timeFrameFormDefaultValues(),
  ...assetFormDefaultValues(),
  ...tradingStreategyDefaultValues(),
  ...srsiSettingsFormDefaultValues(),
};

export const SrsiChartForm = ({ minDate, maxDate }: MaxMinDate) => {
  const fetchData = useQuotesStore((state) => state.fetchCombinedQuotes);
  const { handleSubmit, reset, control } = useForm<ISrsiChartForm>({
    defaultValues,
  });

  const onSubmit = async (data: ISrsiChartForm) => {
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
      srsiSettings: { ...data },
      technicalIndicators: [TechnicalIndicators.Srsi],
    });
  };

  const chartSettingsPanelCss = {
    container: () =>
      css({
        padding: "1rem",
        backgroundColor: "#f9f9f9",
        borderRadius: "8px",
        boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
      }),
    sectionContainer: () =>
      css({
        padding: "1rem",
        backgroundColor: "#ffffff",
        borderRadius: "8px",
        boxShadow: "0 2px 4px rgba(0,0,0,0.1)",
        marginBottom: "1rem",
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
        padding: "1rem",
      }),
    buttonBar: () =>
      css({
        marginTop: "1rem",
        display: "flex",
        justifyContent: "flex-end",
        gap: "1rem",
      }),
  };

  return (
    <Paper css={chartSettingsPanelCss.container}>
      <Box component="form" onSubmit={handleSubmit(onSubmit)}>
        <Box css={chartSettingsPanelCss.sectionContainer}>
          <Typography variant="h6" css={chartSettingsPanelCss.header}>
            General Settings
          </Typography>
          <Box css={chartSettingsPanelCss.form}>
            <AssetFormElements<ISrsiChartForm> control={control} />
            <TradingStreategyFormElement<ISrsiChartForm> control={control} />
            <TimeFrameFormElements<ISrsiChartForm>
              control={control}
              minDate={minDate}
              maxDate={maxDate}
            />
          </Box>
        </Box>
        <Box css={chartSettingsPanelCss.sectionContainer}>
          <Typography variant="h6" css={chartSettingsPanelCss.header}>
            Srsi Custom Settings
          </Typography>
          <Box css={chartSettingsPanelCss.form}>
            <SrsiSettingsFormElements<ISrsiChartForm> control={control} />
          </Box>
        </Box>
        <Box css={chartSettingsPanelCss.buttonBar}>
          <CommonButton text="Submit" type="submit" size="small" />
          <CommonButton text="Reset" type="reset" secondary size="small" onClick={() => reset()} />
        </Box>
      </Box>
    </Paper>
  );
};
