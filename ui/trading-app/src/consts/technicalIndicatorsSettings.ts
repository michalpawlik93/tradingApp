import { MfiSettings } from "../types/MfiSettings";
import { RsiSettings } from "../types/RsiSettings";
import { SRsiSettings } from "../types/SRsiSettings";
import { WaveTrendSettings } from "../types/WaveTrendSettings";

export const sRsiSettingsDefault: SRsiSettings = {
  enable: false,
  channelLength: 10,
  stochKSmooth: 1,
  stochDSmooth: 1,
  oversold: -60,
  overbought: 60,
};

export const waveTrendSettingsDefault: WaveTrendSettings = {
  channelLength: 9,
  averageLength: 12,
  movingAverageLength: 3,
  oversold: -60,
  overbought: 70,
  overboughtLevel2: 20,
  oversoldLevel2: -20,
};

export const rsiSettingsDefault: RsiSettings = {
  oversold: -60,
  overbought: 60,
};

export const mfiSettingsDefault: MfiSettings = {
  channelLength: 60,
};
