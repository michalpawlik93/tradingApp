import { Quote } from "./Quote";
import { SrsiSignal } from "./SrsiSignal";

export interface CombinedQuote {
  ohlc: Quote;
  rsi: number;
  srsiSignal: SrsiSignal;
}
