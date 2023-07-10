import { ApexDateChartCordinate } from "./ApexDateChartCordinate";

export interface ApexCypherBChartData {
  waveTrend: ApexDateChartCordinate[];
  mfi: ApexDateChartCordinate[];
  vwap: ApexDateChartCordinate[];
  lowestY: number;
  highestY: number;
}
