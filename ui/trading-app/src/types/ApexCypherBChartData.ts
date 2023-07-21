import { ApexDateChartCordinate } from "./ApexDateChartCordinate";
import { WaveTrendChartCordinate } from "./WaveTrendChartCordinate";

export interface ApexCypherBChartData {
  waveTrend: WaveTrendChartCordinate[];
  mfi: ApexDateChartCordinate[];
  vwap: ApexDateChartCordinate[];
  lowestY: number;
  highestY: number;
}
