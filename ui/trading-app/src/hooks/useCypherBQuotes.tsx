import { useRef, useEffect } from "react";
import { useQuotesStore } from "../stores/quotesStore";
import { CypherBQuote } from "../types/CypherBQuote";
import { Granularity } from "../consts/granularity";
import { AssetType } from "../consts/assetType";
import { AssetName } from "../consts/assetName";
import {
  mfiSettingsDefault,
  sRsiSettingsDefault,
  waveTrendSettingsDefault,
} from "../consts/technicalIndicatorsSettings";
import { TradingStrategy } from "src/consts/tradingStrategy";
import { cypherBDefaultValues } from "src/components/forms/ChartSettingsPanelForm";

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
