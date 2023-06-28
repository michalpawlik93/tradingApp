import { ApexDateChartCordinate } from "./ApexDateChartCordinate";

export interface ApexCypherBChartData {
  momentumWave: ApexDateChartCordinate[];
  mfi: ApexDateChartCordinate[];
  vwap: ApexDateChartCordinate[];
  lowestY: number;
  highestY: number;
}
