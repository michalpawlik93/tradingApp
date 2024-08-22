import { MfiSettings } from "../../types/MfiSettings";
import { SrsiSettings } from "../../types/SrsiSettings";
import { WaveTrendSettings } from "../../types/WaveTrendSettings";
import { AssetDto } from "./AssetDto";
import { TimeFrameDto } from "./TimeFrameDto";

export interface GetCypherBDto {
  asset: AssetDto;
  timeFrame: TimeFrameDto;
  waveTrendSettings: WaveTrendSettings;
  sRsiSettings: SrsiSettings;
  mfiSettings: MfiSettings;
  tradingStrategy: string;
}
