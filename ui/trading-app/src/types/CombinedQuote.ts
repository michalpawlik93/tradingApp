import { Quote } from "./Quote";

export interface CombinedQuote {
  ohlc: Quote;
  rsi: number;
  sma: number;
}
