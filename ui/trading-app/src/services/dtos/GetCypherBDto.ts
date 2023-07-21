import { AssetDto } from "./AssetDto";
import { TimeFrameDto } from "./TimeFrameDto";
import { WaveTrendSettingsDto } from "./WaveTrendSettingsDto";

export interface GetCypherBDto {
  asset: AssetDto;
  timeFrame: TimeFrameDto;
  waveTrendSettings: WaveTrendSettingsDto;
}
