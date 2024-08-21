import { Quote } from "./Quote";
import { SrsiSignal } from "./SrsiSignal";

export interface SrsiQuote {
  ohlc: Quote;
  srsi: SrsiSignal;
}
