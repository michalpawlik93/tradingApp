import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import {StooqUrls} from "./urls/stooqUrl"

export const StooqDataService = {
  getCombinedQuotes: async (historyType:string): Promise<CombinedQuoteResponse> => {
    try {
      const response = await fetch(`${StooqUrls.combinedQuote.getAll}?historyType=${historyType}`);
      const json= await response.json();
      return Promise.resolve(json.data);
    } catch (error) {
      throw new Error("Error retrieving data from the server");
    }
  },
};