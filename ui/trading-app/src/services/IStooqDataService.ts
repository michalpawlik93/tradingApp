import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import { CypherBResponse } from "../types/CypherBResponse";
import { GetQuotesRequestDto } from "./dtos/GetQuotesRequestDto";

export interface IStooqDataService {
  getCombinedQuotes: (
    request: GetQuotesRequestDto
  ) => Promise<CombinedQuoteResponse>;
  getCypherB: (request: GetQuotesRequestDto) => Promise<CypherBResponse>;
}
