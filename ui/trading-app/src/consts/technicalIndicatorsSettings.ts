import { MfiSettings } from "../types/MfiSettings";
import { RsiSettings } from "../types/RsiSettings";
import { SrsiSettings } from "../types/SrsiSettings";
import { WaveTrendSettings } from "../types/WaveTrendSettings";

export const sRsiSettingsDefault = (): SrsiSettings => ({
  enabled: false,
  channelLength: 10,
  stochKSmooth: 1,
  stochDSmooth: 1,
  oversold: 20,
  overbought: 60,
});

export const waveTrendSettingsDefault: WaveTrendSettings = {
  channelLength: 10,
  averageLength: 21,
  movingAverageLength: 3,
  oversold: -60,
  overbought: 60,
  overboughtLevel2: 50,
  oversoldLevel2: -53,
};

export const rsiSettingsDefault: RsiSettings = {
  oversold: -60,
  overbought: 60,
};

export const mfiSettingsDefault: MfiSettings = {
  channelLength: 60,
};
