import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import { StooqUrls } from "./urls/stooqUrl";
import { IStooqDataService } from "./IStooqDataService";
import { GetQuotesDtoRequest } from "./dtos/GetQuotesDtoRequest";
import { CypherBResponse } from "../types/CypherBResponse";

export const StooqDataService: IStooqDataService = {
  getCombinedQuotes: async (
    request: GetQuotesDtoRequest
  ): Promise<CombinedQuoteResponse> => {
    const { Granularity, AssetType, AssetName, StartDate, EndDate } = request;
    const url = `${StooqUrls.combinedQuote.getAll}?granularity=${Granularity}&assetType=${AssetType}&assetName=${AssetName}&startDate=${StartDate}&endDate=${EndDate}`;
    return fetchData(url);
  },
  getCypherB: async (
    request: GetQuotesDtoRequest
  ): Promise<CypherBResponse> => {
    const { Granularity, AssetType, AssetName, StartDate, EndDate } = request;
    const url = `${StooqUrls.cypherB.get}?granularity=${Granularity}&assetType=${AssetType}&assetName=${AssetName}&startDate=${StartDate}&endDate=${EndDate}`;
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
