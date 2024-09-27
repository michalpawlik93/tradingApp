import { css } from "@emotion/react";
import { Box, Paper, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import {
  mfiSettingsDefault,
  sRsiSettingsDefault,
  waveTrendSettingsDefault,
} from "../../consts/technicalIndicatorsSettings";
import { TradingStrategy } from "../../consts/tradingStrategy";
import { useQuotesFormStore } from "../../stores/quotesFormStore";
import { useQuotesStore } from "../../stores/quotesStore";
import { MaxMinDate } from "../../types/MaxMinDate";
import { CommonButton } from "../presentational/Button";
import { AssetFormElements, IAssetFormValues } from "./AssetFormElements";
import { ITimeFrameFormValues, TimeFrameFormElements } from "./TimeFrameFormElements";
import {
  ITradingStreategyValues,
  TradingStreategyFormElement,
} from "./TradingStreategyFormElement";

export interface ICypherBChartForm
  extends ITimeFrameFormValues,
    IAssetFormValues,
    ITradingStreategyValues {}

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

export const CypherBChartForm = ({ minMaxDate }: { minMaxDate: MaxMinDate }) => {
  const fetchData = useQuotesStore((state) => state.fetchCypherBQuotes);
  const formData = useQuotesFormStore((state) => state.cipherBForm);
  const setFormData = useQuotesFormStore((state) => state.setCipherBForm);
  const { handleSubmit, reset, control } = useForm<ICypherBChartForm>({
    defaultValues: formData,
  });
  const onSubmit = async (data: ICypherBChartForm) => {
    setFormData(data);
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
      settings: {
        waveTrendSettings: waveTrendSettingsDefault,
        srsiSettings: sRsiSettingsDefault(),
        mfiSettings: mfiSettingsDefault,
      },
    });
  };

  return (
    <Paper css={chartSettingsPanelCss.container}>
      <Typography variant="h6" css={chartSettingsPanelCss.header}>
        Chart Settings
      </Typography>
      <Box component="form" onSubmit={handleSubmit(onSubmit)} css={chartSettingsPanelCss.form}>
        <AssetFormElements<ICypherBChartForm> control={control} />
        <TradingStreategyFormElement<ICypherBChartForm> control={control} />
        <TimeFrameFormElements<ICypherBChartForm>
          control={control}
          minDate={minMaxDate.minDate}
          maxDate={minMaxDate.maxDate}
        />
        <Box css={chartSettingsPanelCss.buttonBar}>
          <CommonButton text="Submit" type="submit" size="small" />
          <CommonButton text="Reset" type="reset" secondary size="small" onClick={() => reset()} />
        </Box>
      </Box>
    </Paper>
  );
};
