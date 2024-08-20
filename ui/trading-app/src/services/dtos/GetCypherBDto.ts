import { MfiSettings } from "../../types/MfiSettings";
import { SRsiSettings } from "../../types/SRsiSettings";
import { WaveTrendSettings } from "../../types/WaveTrendSettings";
import { AssetDto } from "./AssetDto";
import { TimeFrameDto } from "./TimeFrameDto";

export interface GetCypherBDto {
  asset: AssetDto;
  timeFrame: TimeFrameDto;
  waveTrendSettings: WaveTrendSettings;
  sRsiSettings: SRsiSettings;
  mfiSettings: MfiSettings;
  tradingStrategy: string;
}
