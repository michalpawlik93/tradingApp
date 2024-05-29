export interface OhlcChartData {
  categoryData: string[];
  values: number[][];
}

export interface WaveTrendChartData {
  waveTrendWt1: (number | Date)[][];
  waveTrendWt2: (number | Date)[][];
  sellSignals: (number | Date)[][];
  buySignals: (number | Date)[][];
}

export interface RsiChartData {
  overbought: number;
  oversold: number;
  rsi: [string, number][];
}