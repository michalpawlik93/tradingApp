import { useEffect, useRef } from "react";
import { cypherBDefaultValues } from "../components/forms/ChartSettingsPanelForm";
import {
  mfiSettingsDefault,
  sRsiSettingsDefault,
  waveTrendSettingsDefault,
} from "../consts/technicalIndicatorsSettings";
import { useQuotesStore } from "../stores/quotesStore";
import { CypherBQuote } from "../types/CypherBQuote";

export interface useCypherBQuotesResponse {
  cypherBQuotes: CypherBQuote[];
}

export const useCypherBQuotes = (): useCypherBQuotesResponse => {
  const cypherBQuotes = useQuotesStore((state) => state.cypherBQuotes);
  const isDataFetched = useRef(false);
  const fetchData = useQuotesStore((state) => state.fetchCypherBQuotes);

  useEffect(() => {
    async function fetch() {
      if (isDataFetched.current) {
        isDataFetched.current = true;
        return;
      }
      isDataFetched.current = true;
      await fetchData({
        asset: {
          name: cypherBDefaultValues.assetName,
          type: cypherBDefaultValues.assetType,
        },
        timeFrame: {
          granularity: cypherBDefaultValues.granularity,
          startDate: cypherBDefaultValues.startDate.toISOString(),
          endDate: cypherBDefaultValues.endDate.toISOString(),
        },
        waveTrendSettings: waveTrendSettingsDefault,
        sRsiSettings: sRsiSettingsDefault,
        mfiSettings: mfiSettingsDefault,
        tradingStrategy: cypherBDefaultValues.tradingStrategy,
      });
    }
    fetch();
  }, [fetchData]);

  return { cypherBQuotes };
};
