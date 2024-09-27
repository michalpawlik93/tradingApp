import { MfiSettings } from "../../types/MfiSettings";
import { RsiSettings } from "../../types/RsiSettings";
import { SrsiSettings } from "../../types/SrsiSettings";
import { WaveTrendSettings } from "../../types/WaveTrendSettings";

export interface SettingsDto {
  srsiSettings?: SrsiSettings;
  rsiSettings?: RsiSettings;
  mfiSettings?: MfiSettings;
  waveTrendSettings?: WaveTrendSettings;
}
