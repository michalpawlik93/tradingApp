import { SrsiSettings } from "../../types/SrsiSettings";
import { AssetDto } from "./AssetDto";
import { TimeFrameDto } from "./TimeFrameDto";

export interface GetCombinedQuotesRequestDto {
  technicalIndicators: string[];
  asset: AssetDto;
  timeFrame: TimeFrameDto;
  srsiSettings?: SrsiSettings;
}
