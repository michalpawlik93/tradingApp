import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import { CypherBResponse } from "../types/CypherBResponse";

export interface IStooqDataService {
  getCombinedQuotes: (granularity: string) => Promise<CombinedQuoteResponse>;
  getCypherB: (granularity: string) => Promise<CypherBResponse>;
}
