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
  channelLength: 0,
  averageLength: 0,
  movingAverageLength: 0,
  oversold: 0,
  overbought: 0,
};

export const rsiSettingsDefault: RsiSettings = {
  oversold: 0,
  overbought: 0,
};
