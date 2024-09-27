import { AssetDto } from "./AssetDto";
import { SettingsDto } from "./SettingsDto";
import { TimeFrameDto } from "./TimeFrameDto";

export interface GetCypherBDto {
  asset: AssetDto;
  timeFrame: TimeFrameDto;
  settings: SettingsDto;
}
