import { create } from "zustand";
import { StooqDataService } from "../services/StooqDataService";
import { CombinedQuote } from "../types/CombinedQuote";
import { CypherBQuote } from "../types/CypherBQuote";
import { RsiSettings } from "../types/RsiSettings";
import { GetQuotesDtoRequest } from "../services/dtos/GetQuotesDtoRequest";

interface StooqState {
  combinedQuotes: CombinedQuote[];
  cypherBQuotes: CypherBQuote[];
  rsiSettings: RsiSettings;
  fetchCombinedQuotes: (request: GetQuotesDtoRequest) => Promise<void>;
  fetchCypherBQuotes: (request: GetQuotesDtoRequest) => Promise<void>;
}

export const useStooqStore = create<StooqState>((set) => ({
  combinedQuotes: [],
  cypherBQuotes: [],
  rsiSettings: {
    overbought: 0,
    oversold: 0,
  },
  fetchCombinedQuotes: async (request: GetQuotesDtoRequest) => {
    try {
      const response = await StooqDataService.getCombinedQuotes(request);
      set({
        combinedQuotes: response.quotes,
        rsiSettings: response.rsiSettings,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
  fetchCypherBQuotes: async (request: GetQuotesDtoRequest) => {
    try {
      const response = await StooqDataService.getCypherB(request);
      set({
        cypherBQuotes: response.quotes,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
}));
