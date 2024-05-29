import { create } from "zustand";
import { QuotesDataService } from "../services/QuotesDataService";
import { CombinedQuote } from "../types/CombinedQuote";
import { CypherBQuote } from "../types/CypherBQuote";
import { GetQuotesRequestDto } from "../services/dtos/GetQuotesRequestDto";
import { GetCypherBDto } from "../services/dtos/GetCypherBDto";
import { rsiSettingsDefault } from "../consts/technicalIndicatorsSettings";
import { RsiSettings } from "../types/RsiSettings";

interface QuotesState {
  combinedQuotes: CombinedQuote[];
  cypherBQuotes: CypherBQuote[];
  rsiSettings: RsiSettings;
  fetchCombinedQuotes: (request: GetQuotesRequestDto) => Promise<void>;
  fetchCypherBQuotes: (request: GetCypherBDto) => Promise<void>;
}

export const useQuotesStore = create<QuotesState>((set) => ({
  combinedQuotes: [],
  cypherBQuotes: [],
  rsiSettings: rsiSettingsDefault,
  fetchCombinedQuotes: async (request: GetQuotesRequestDto) => {
    try {
      const response = await QuotesDataService.getCombinedQuotes(request);
      set({
        combinedQuotes: response.quotes,
        rsiSettings: response.rsiSettings ?? rsiSettingsDefault,
      });
    } catch (error) {
      console.error("Error fetching combined quotes:", error);
    }
  },
  fetchCypherBQuotes: async (request: GetCypherBDto) => {
    try {
      const response = await QuotesDataService.getCypherB(request);
      set({
        cypherBQuotes: response.quotes,
      });
    } catch (error) {
      console.error("Error fetching cypherB quotes:", error);
    }
  },
}));
