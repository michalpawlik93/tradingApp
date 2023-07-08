import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import { StooqUrls } from "./urls/stooqUrl";
import { IStooqDataService } from "./IStooqDataService";
import { GetQuotesDtoRequest } from "./dtos/GetQuotesDtoRequest";
import { CypherBResponse } from "../types/CypherBResponse";

export const StooqDataService: IStooqDataService = {
  getCombinedQuotes: async (
    request: GetQuotesDtoRequest
  ): Promise<CombinedQuoteResponse> => {
    const { granularity, assetType, assetName, startDate, endDate } = request;
    const url = `${StooqUrls.combinedQuote.getAll}?granularity=${granularity}&assetType=${assetType}&assetName=${assetName}&startDate=${startDate}&endDate=${endDate}`;
    return fetchData(url);
  },
  getCypherB: async (
    request: GetQuotesDtoRequest
  ): Promise<CypherBResponse> => {
    const { granularity, assetType, assetName, startDate, endDate } = request;
    const url = `${StooqUrls.cypherB.get}?granularity=${granularity}&assetType=${assetType}&assetName=${assetName}&startDate=${startDate}&endDate=${endDate}`;
    return fetchData(url);
  },
};

async function fetchData<T>(url: string): Promise<T> {
  try {
    const response = await fetch(url);
    const json = await response.json();
    return Promise.resolve(json.data as T);
  } catch (error) {
    throw new Error("Error retrieving data from the server");
  }
}
