import { ApexDateChartCordinate } from "./ApexDateChartCordinate";
import { WaveTrendChartCordinate } from "./WaveTrendChartCordinate";

export interface ApexCypherBChartData {
  waveTrendWt1: WaveTrendChartCordinate[];
  waveTrendWt2: ApexDateChartCordinate[];
  waveTrendVwap: ApexDateChartCordinate[];
  mfi: ApexDateChartCordinate[];
  vwap: ApexDateChartCordinate[];
  lowestY: number;
  highestY: number;
}
