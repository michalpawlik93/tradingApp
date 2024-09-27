import { create } from "zustand";
import { TechnicalIndicators } from "../consts/technicalIndicators";
import { rsiSettingsDefault } from "../consts/technicalIndicatorsSettings";
import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { GetCypherBDto } from "../services/dtos/GetCypherBDto";
import { QuotesDataService } from "../services/QuotesDataService";
import { CypherBQuote } from "../types/CypherBQuote";
import { RsiQuote } from "../types/RsiQuote";
import { RsiSettings } from "../types/RsiSettings";
import { SrsiQuote } from "../types/SrsiQuote";

interface QuotesState {
  srsiQuotes: SrsiQuote[];
  rsiQuotes: RsiQuote[];
  cypherBQuotes: CypherBQuote[];
  rsiSettings: RsiSettings;
  fetchCombinedQuotes: (request: GetCombinedQuotesRequestDto) => Promise<void>;
  fetchCypherBQuotes: (request: GetCypherBDto) => Promise<void>;
}

export const useQuotesStore = create<QuotesState>((set) => ({
  srsiQuotes: [],
  rsiQuotes: [],
  cypherBQuotes: [],
  rsiSettings: rsiSettingsDefault,
  fetchCombinedQuotes: async (request: GetCombinedQuotesRequestDto) => {
    try {
      let serviceRequest = request;
      if (!request?.settings?.srsiSettings?.enabled) {
        serviceRequest = { ...request, settings: { srsiSettings: undefined } };
      }
      const response = await QuotesDataService.getCombinedQuotes(serviceRequest);
      if (request.indicators.map((x) => x.technicalIndicator).includes(TechnicalIndicators.Rsi)) {
        set({
          rsiQuotes: response.quotes,
          rsiSettings: response.rsiSettings ?? rsiSettingsDefault,
        });
      }
      if (request.indicators.map((x) => x.technicalIndicator).includes(TechnicalIndicators.Srsi)) {
        set({
          srsiQuotes: response.quotes,
        });
      }
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
