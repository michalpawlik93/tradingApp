import { Quote } from "./Quote";

export interface CypherBQuote {
  ohlc: Quote;
  momentumWave: number;
  mfi: number;
  vwap: number;
}
