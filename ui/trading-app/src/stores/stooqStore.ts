import { create } from "zustand";
import { StooqDataService } from "../services/StooqDataService";
import { CombinedQuote } from "../types/CombinedQuote";
import { CypherBQuote } from "../types/CypherBQuote";
import { RsiSettings } from "../types/RsiSettings";
import { GetQuotesRequestDto } from "../services/dtos/GetQuotesRequestDto";

interface StooqState {
  combinedQuotes: CombinedQuote[];
  cypherBQuotes: CypherBQuote[];
  rsiSettings: RsiSettings;
  fetchCombinedQuotes: (request: GetQuotesRequestDto) => Promise<void>;
  fetchCypherBQuotes: (request: GetQuotesRequestDto) => Promise<void>;
}

export const useStooqStore = create<StooqState>((set) => ({
  combinedQuotes: [],
  cypherBQuotes: [],
  rsiSettings: {
    overbought: 0,
    oversold: 0,
  },
  fetchCombinedQuotes: async (request: GetQuotesRequestDto) => {
    try {
      const response = await StooqDataService.getCombinedQuotes(request);
      set({
        combinedQuotes:
          response.quotes.length > 1000
            ? response.quotes.slice(0, 1000)
            : response.quotes,
        rsiSettings: response.rsiSettings,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
  fetchCypherBQuotes: async (request: GetQuotesRequestDto) => {
    try {
      const response = await StooqDataService.getCypherB(request);
      set({
        cypherBQuotes:
          response.quotes.length > 1000
            ? response.quotes.slice(0, 1000)
            : response.quotes,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
}));
