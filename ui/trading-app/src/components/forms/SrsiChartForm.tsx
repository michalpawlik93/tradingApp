import { css } from "@emotion/react";
import { Box, Paper, Typography } from "@mui/material";
import { useForm } from "react-hook-form";
import { TechnicalIndicators } from "../../consts/technicalIndicators";
import { useQuotesFormStore } from "../../stores/quotesFormStore";
import { useQuotesStore } from "../../stores/quotesStore";
import { MaxMinDate } from "../../types/MaxMinDate";
import { CommonButton } from "../presentational/Button";
import { AssetFormElements, IAssetFormValues } from "./AssetFormElements";
import { ISrsiSettingsFormValues, SrsiSettingsFormElements } from "./SrsiSettingsFormElements";
import { ITimeFrameFormValues, TimeFrameFormElements } from "./TimeFrameFormElements";
import {
  ITradingStreategyValues,
  TradingStreategyFormElement,
} from "./TradingStreategyFormElement";

export interface ISrsiChartForm
  extends ITimeFrameFormValues,
    IAssetFormValues,
    ITradingStreategyValues,
    ISrsiSettingsFormValues {}

export const SrsiChartForm = ({ minMaxDate }: { minMaxDate: MaxMinDate }) => {
  const fetchData = useQuotesStore((state) => state.fetchCombinedQuotes);
  const formData = useQuotesFormStore((state) => state.srsiForm);
  const setFormData = useQuotesFormStore((state) => state.setSrsisForm);

  const { handleSubmit, reset, control } = useForm<ISrsiChartForm>({
    defaultValues: formData,
  });

  const onSubmit = async (data: ISrsiChartForm) => {
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
              minDate={minMaxDate.minDate}
              maxDate={minMaxDate.maxDate}
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
