import { GetCombinedQuotesRequestDto } from "./dtos/GetCombinedQuotesRequestDto";
import { GetCombinedQuotesResponseDto } from "./dtos/GetCombinedQuotesResponseDto";
import { GetCypherBDto } from "./dtos/GetCypherBDto";
import { GetCypherBResponseDto } from "./dtos/GetCypherBResponseDto";

export interface IQuotesDataService {
  getCombinedQuotes: (
    request: GetCombinedQuotesRequestDto,
  ) => Promise<GetCombinedQuotesResponseDto>;
  getCypherB: (request: GetCypherBDto) => Promise<GetCypherBResponseDto>;
}
