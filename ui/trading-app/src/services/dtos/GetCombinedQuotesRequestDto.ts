import { AssetDto } from "./AssetDto";
import { SettingsDto } from "./SettingsDto";
import { TimeFrameDto } from "./TimeFrameDto";

export interface GetCombinedQuotesRequestDto {
  indicators: IndicatorsDto[];
  asset: AssetDto;
  timeFrame: TimeFrameDto;
  settings: SettingsDto;
}

interface IndicatorsDto {
  technicalIndicator: string;
  sideIndicators: string[];
}
