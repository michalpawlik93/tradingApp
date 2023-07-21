import { ApexDateChartCordinate } from "./ApexDateChartCordinate";

export interface WaveTrendChartCordinate extends ApexDateChartCordinate {
  crossesOver: boolean;
  crossesUnder: boolean;
}
