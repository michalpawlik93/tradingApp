import { CombinedQuote } from "./CombinedQuote";
import { RsiSettings } from "./RsiSettings";

export interface CombinedQuoteResponse {
  quotes: CombinedQuote[];
  rsiSettings: RsiSettings;
}
