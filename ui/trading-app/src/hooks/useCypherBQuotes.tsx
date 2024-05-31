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
          name: AssetName.BTC,
          type: AssetType.Cryptocurrency,
        },
        timeFrame: {
          granularity: Granularity.Hourly,
          startDate: new Date(2023, 5, 24).toISOString(),
          endDate: new Date(2023, 8, 28).toISOString(),
        },
        waveTrendSettings: waveTrendSettingsDefault,
        sRsiSettings: sRsiSettingsDefault,
        mfiSettings: mfiSettingsDefault,
      });
    }
    fetch();
  }, [fetchData]);

  return { cypherBQuotes };
};
