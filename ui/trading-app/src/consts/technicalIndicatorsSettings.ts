import { RsiSettings } from "../types/RsiSettings";
import { SRsiSettings } from "../types/SRsiSettings";
import { WaveTrendSettings } from "../types/WaveTrendSettings";

export const sRsiSettingsDefault: SRsiSettings = {
  enable: false,
  length: 0,
  stochKSmooth: 0,
  stochDSmooth: 0,
};

export const waveTrendSettingsDefault: WaveTrendSettings = {
  channelLength: 10,
  averageLength: 21,
  movingAverageLength: 3,
  oversold: -60,
  overbought: 60,
};

export const rsiSettingsDefault: RsiSettings = {
  oversold: -60,
  overbought: 60,
};
