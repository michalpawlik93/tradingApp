import { create } from "zustand";
import { StooqDataService } from "../services/StooqDataService";
import { CombinedQuote } from "../types/CombinedQuote";
import { CypherBQuote } from "../types/CypherBQuote";
import { RsiSettings } from "../types/RsiSettings";

interface StooqState {
  combinedQuotes: CombinedQuote[];
  cypherBQuotes: CypherBQuote[];
  rsiSettings: RsiSettings;
  fetchCombinedQuotes: (historyType: string) => Promise<void>;
  fetchCypherBQuotes: (granularity: string) => Promise<void>;
}

export const useStooqStore = create<StooqState>((set) => ({
  combinedQuotes: [],
  cypherBQuotes: [],
  rsiSettings: {
    overbought: 0,
    oversold: 0,
  },
  fetchCombinedQuotes: async (granularity: string) => {
    try {
      const response = await StooqDataService.getCombinedQuotes(granularity);
      set({
        combinedQuotes: response.quotes,
        rsiSettings: response.rsiSettings,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
  fetchCypherBQuotes: async (granularity: string) => {
    try {
      const response = await StooqDataService.getCypherB(granularity);
      set({
        cypherBQuotes: response.quotes,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
}));
