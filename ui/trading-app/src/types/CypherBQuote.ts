import { MfiResult } from "./Mfi";
import { Quote } from "./Quote";
import { SrsiSignal } from "./SrsiSignal";
import { WaveTrendSignal } from "./WaveTrendSignal";

export interface CypherBQuote {
  ohlc: Quote;
  waveTrendSignal: WaveTrendSignal;
  mfiResult: MfiResult;
  srsiSignal: SrsiSignal;
}
