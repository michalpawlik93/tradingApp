import { GetCombinedQuotesRequestDto } from "./dtos/GetCombinedQuotesRequestDto";
import { GetCombinedQuotesResponseDto } from "./dtos/GetCombinedQuotesResponseDto";
import { GetCypherBDto } from "./dtos/GetCypherBDto";
import { GetCypherBResponseDto } from "./dtos/GetCypherBResponseDto";
import { IQuotesDataService } from "./IQuotesDataService";
import { StooqUrls } from "./urls/stooqUrl";

export const QuotesDataService: IQuotesDataService = {
  getCombinedQuotes: async (
    request: GetCombinedQuotesRequestDto,
  ): Promise<GetCombinedQuotesResponseDto> => {
    const url = `${StooqUrls.combinedQuote.getAll}`;
    return await fetchData(url, "POST", request);
  },
  getCypherB: async (request: GetCypherBDto): Promise<GetCypherBResponseDto> => {
    const url = `${StooqUrls.cypherB.get}`;
    return await fetchData(url, "POST", request);
  },
};

async function fetchData<TInput, TOutput>(
  url: string,
  method: string,
  requestData: TInput,
): Promise<TOutput> {
  try {
    const response = await fetch(url, {
      mode: "cors",
      method,
      headers: {
        "Content-Type": "application/json",
      },
      body: requestData ? JSON.stringify(requestData) : undefined,
    });
    const json = await response.json();
    return json.data as TOutput;
  } catch (error) {
    throw new Error(`Error retrieving data from the server: ${error}`);
  }
}
