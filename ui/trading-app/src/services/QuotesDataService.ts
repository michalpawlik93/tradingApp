import { StooqUrls } from "./urls/stooqUrl";
import { IQuotesDataService } from "./IQuotesDataService";
import { GetQuotesRequestDto } from "./dtos/GetQuotesRequestDto";
import { GetCombinedQuotesResponseDto } from "./dtos/GetCombinedQuotesResponseDto";
import { GetCypherBResponseDto } from "./dtos/GetCypherBResponseDto";
import { GetCypherBDto } from "./dtos/GetCypherBDto";

export const QuotesDataService: IQuotesDataService = {
  getCombinedQuotes: async (
    request: GetQuotesRequestDto
  ): Promise<GetCombinedQuotesResponseDto> => {
    const { granularity, assetType, assetName, startDate, endDate } = request;
    const url = `${StooqUrls.combinedQuote.getAll}?granularity=${granularity}&assetType=${assetType}&assetName=${assetName}&startDate=${startDate}&endDate=${endDate}`;
    return fetchData(url, "GET", undefined);
  },
  getCypherB: async (
    request: GetCypherBDto
  ): Promise<GetCypherBResponseDto> => {
    const url = `${StooqUrls.cypherB.get}`;
    return fetchData(url, "POST", request);
  },
};

async function fetchData<TInput, TOutput>(
  url: string,
  method: string,
  requestData: TInput
): Promise<TOutput> {
  try {
    const response = await fetch(url, {
      method: method,
      headers: {
        "Content-Type": "application/json",
      },
      body: requestData ? JSON.stringify(requestData) : undefined,
    });
    const json = await response.json();
    return Promise.resolve(json.data as TOutput);
  } catch (error) {
    throw new Error(`Error retrieving data from the server: ${error}`);
  }
}
