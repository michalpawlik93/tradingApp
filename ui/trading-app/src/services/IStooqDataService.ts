import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import { CypherBResponse } from "../types/CypherBResponse";
import { GetQuotesDtoRequest } from "./dtos/GetQuotesRequestDto";

export interface IStooqDataService {
  getCombinedQuotes: (
    request: GetQuotesDtoRequest
  ) => Promise<CombinedQuoteResponse>;
  getCypherB: (request: GetQuotesDtoRequest) => Promise<CypherBResponse>;
}
