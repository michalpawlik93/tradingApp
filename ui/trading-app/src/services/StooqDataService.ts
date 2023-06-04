import axios from "axios";
import { CombinedQuoteResponse } from "../types/CombinedQuoteResponse";

export const StooqDataService = {
  getCombinedQuotes: async (): Promise<CombinedQuoteResponse> => {
    try {
      const response = await axios.get("https://api.example.com/data");
      return response.data; 
    } catch (error) {
      throw new Error("Error retrieving data from the server");
    }
  },
};

