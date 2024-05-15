import { CombinedQuote } from "../../types/CombinedQuote";
import { RsiSettings } from "../../types/RsiSettings";

export interface GetCombinedQuotesResponseDto {
  quotes: CombinedQuote[];
  rsiSettings: RsiSettings;
}
