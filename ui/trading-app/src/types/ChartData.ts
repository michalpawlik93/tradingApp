export interface OhlcChartData {
  ohlc: [number, number, number, number, number][];
}

export interface WaveTrendChartData {
  waveTrendWt1: (number | Date)[][];
  waveTrendWt2: (number | Date)[][];
  waveTrendVwap: (number | Date)[][];
  waveTrendSell: (number | Date)[][];
  waveTrendBuy: (number | Date)[][];
}

export interface RsiChartData {
  overbought: number;
  oversold: number;
  rsi: (number | Date)[][];
}

interface MfiChartData {
  mfiBuy: (number | Date)[][];
  mfiSell: (number | Date)[][];
}

export interface SrsiChartData {
  srsiStochK: (number | Date)[][];
  srsiStochD: (number | Date)[][];
  srsiSell: (number | Date)[][];
  srsiBuy: (number | Date)[][];
}

export interface SrsiStandaloneChartData extends SrsiChartData {
  close: (number | Date)[][];
}

export interface CypherBChartData
  extends MfiChartData,
    WaveTrendChartData,
    OhlcChartData,
    SrsiChartData {}
