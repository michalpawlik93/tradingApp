import { create } from "zustand";
import { rsiSettingsDefault } from "../consts/technicalIndicatorsSettings";
import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { GetCypherBDto } from "../services/dtos/GetCypherBDto";
import { QuotesDataService } from "../services/QuotesDataService";
import { CombinedQuote } from "../types/CombinedQuote";
import { CypherBQuote } from "../types/CypherBQuote";
import { RsiSettings } from "../types/RsiSettings";

interface QuotesState {
  combinedQuotes: CombinedQuote[];
  cypherBQuotes: CypherBQuote[];
  rsiSettings: RsiSettings;
  fetchCombinedQuotes: (request: GetCombinedQuotesRequestDto) => Promise<void>;
  fetchCypherBQuotes: (request: GetCypherBDto) => Promise<void>;
}

export const useQuotesStore = create<QuotesState>((set) => ({
  combinedQuotes: [],
  srsiQuotes: [],
  cypherBQuotes: [],
  rsiSettings: rsiSettingsDefault,
  fetchCombinedQuotes: async (request: GetCombinedQuotesRequestDto) => {
    try {
      let serviceRequest = request;
      if (!request?.srsiSettings?.enabled) {
        serviceRequest = { ...request, srsiSettings: undefined };
      }
      const response = await QuotesDataService.getCombinedQuotes(serviceRequest);
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
