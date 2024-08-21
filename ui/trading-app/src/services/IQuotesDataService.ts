import { GetCombinedQuotesResponseDto } from "./dtos/GetCombinedQuotesResponseDto";
import { GetCypherBDto } from "./dtos/GetCypherBDto";
import { GetCypherBResponseDto } from "./dtos/GetCypherBResponseDto";
import { GetQuotesRequestDto } from "./dtos/GetQuotesRequestDto";

export interface IQuotesDataService {
  getCombinedQuotes: (request: GetQuotesRequestDto) => Promise<GetCombinedQuotesResponseDto>;
  getCypherB: (request: GetCypherBDto) => Promise<GetCypherBResponseDto>;
}
