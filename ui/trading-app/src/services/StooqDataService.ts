import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import { StooqUrls } from "./urls/stooqUrl";
import { IStooqDataService } from "./IStooqDataService";
import { CypherBResponse } from "../types/CypherBResponse";

export const StooqDataService: IStooqDataService = {
  getCombinedQuotes: async (
    granularity: string
  ): Promise<CombinedQuoteResponse> => {
    const url = `${StooqUrls.combinedQuote.getAll}?granularity=${granularity}`;
    return fetchData(url);
  },
  getCypherB: async (granularity: string): Promise<CypherBResponse> => {
    const url = `${StooqUrls.cypherB.get}?granularity=${granularity}`;
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
