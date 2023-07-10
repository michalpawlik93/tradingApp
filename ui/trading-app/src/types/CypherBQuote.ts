import { Quote } from "./Quote";
import { WaveTrend } from "./WaveTrend";

export interface CypherBQuote {
  ohlc: Quote;
  waveTrend: WaveTrend;
  mfi: number;
  vwap: number;
}
