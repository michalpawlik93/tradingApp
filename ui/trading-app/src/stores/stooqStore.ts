import {create} from "zustand";
import { StooqDataService } from "../services/StooqDataService";
import { CombinedQuote } from "../types/CombinedQuote";
import { RsiSettings } from "../types/RsiSettings";

interface StooqState {
  combinedQuotes: CombinedQuote[];
  rsiSettings: RsiSettings;
  fetchData: () => Promise<void>;
}

export const useStooqStore = create<StooqState>((set) => ({
  combinedQuotes: [],
  rsiSettings: {
    overbought: 0,
    oversold: 0,
  },
  fetchData: async () => {
    try {
      const response = await StooqDataService.getCombinedQuotes();
      set({
        combinedQuotes: response.quotes,
        rsiSettings: response.rsiSettings,
      });
    } catch (error) {
      console.error("Error fetching data:", error);
    }
  },
}));
