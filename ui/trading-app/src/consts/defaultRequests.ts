import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { AssetName } from "./assetName";
import { AssetType } from "./assetType";
import { Granularity } from "./granularity";
import { TechnicalIndicators } from "./technicalIndicators";
import { sRsiSettingsDefault } from "./technicalIndicatorsSettings";

export const GetCombinedQuotesRequestDtoDefault = (): GetCombinedQuotesRequestDto => ({
  technicalIndicators: [TechnicalIndicators.Rsi, TechnicalIndicators.Srsi],
  asset: {
    name: AssetName.BTC,
    type: AssetType.Cryptocurrency,
  },
  timeFrame: {
    granularity: Granularity.Daily,
    startDate: new Date(2023, 4, 24).toISOString(),
    endDate: new Date(2023, 6, 24).toISOString(),
  },
  srsiSettings: sRsiSettingsDefault,
});
