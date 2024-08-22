import { assetFormDefaultValues } from "../components/forms/AssetFormElements";
import { timeFrameFormDefaultValues } from "../components/forms/TimeFrameFormElements";
import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { TechnicalIndicators } from "./technicalIndicators";
import { sRsiSettingsDefault } from "./technicalIndicatorsSettings";

export const GetCombinedQuotesRequestDtoDefault = (): GetCombinedQuotesRequestDto => ({
  technicalIndicators: [TechnicalIndicators.Rsi, TechnicalIndicators.Srsi],
  asset: {
    name: assetFormDefaultValues().assetName,
    type: assetFormDefaultValues().assetType,
  },
  timeFrame: {
    granularity: timeFrameFormDefaultValues().granularity,
    startDate: timeFrameFormDefaultValues().startDate.toISOString(),
    endDate: timeFrameFormDefaultValues().endDate.toISOString(),
  },
  srsiSettings: sRsiSettingsDefault(),
});
