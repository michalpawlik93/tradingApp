import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";
import {StooqUrls} from "./urls/stooqUrl"

export const StooqDataService = {
  getCombinedQuotes: async (): Promise<CombinedQuoteResponse> => {
    try {
      const response = await fetch("http://localhost:5244/stooq/combinedquote/getall");
      const json= await response.json();
      return Promise.resolve(json.data);
    } catch (error) {
      throw new Error("Error retrieving data from the server");
    }
  },
};