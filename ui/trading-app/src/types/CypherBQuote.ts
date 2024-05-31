import { Mfi } from "./Mfi";
import { Quote } from "./Quote";
import { WaveTrend } from "./WaveTrend";

export interface CypherBQuote {
  ohlc: Quote;
  waveTrend: WaveTrend;
  mfi: Mfi;
}
