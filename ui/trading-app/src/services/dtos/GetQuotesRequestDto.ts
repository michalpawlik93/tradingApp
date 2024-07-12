import { AssetDto } from "./AssetDto";
import { TimeFrameDto } from "./TimeFrameDto";

export interface GetQuotesRequestDto {
  technicalIndicators: string[];
  asset: AssetDto;
  timeFrame: TimeFrameDto;
}
