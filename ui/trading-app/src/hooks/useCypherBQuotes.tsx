import { useRef, useEffect } from "react";
import { useQuotesStore } from "../stores/quotesStore";
import { CypherBQuote } from "../types/CypherBQuote";
import { Granularity } from "../consts/granularity";
import { AssetType } from "../consts/assetType";
import { AssetName } from "../consts/assetName";
import {
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
      if (!isDataFetched.current) {
        isDataFetched.current = true;
        return;
      }
      await fetchData({
        asset: {
          name: AssetName.BTC,
          type: AssetType.Cryptocurrency,
        },
        timeFrame: {
          granularity: Granularity.Hourly,
        },
        waveTrendSettings: waveTrendSettingsDefault,
        sRsiSettings: sRsiSettingsDefault,
      });
    }
    fetch();
  }, [fetchData]);

  return { cypherBQuotes };
};
