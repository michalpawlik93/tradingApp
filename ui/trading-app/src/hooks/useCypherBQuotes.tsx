import { useRef, useEffect } from "react";
import { useStooqStore } from "../stores/stooqStore";
import { CypherBQuote } from "../types/CypherBQuote";
import { Granularity } from "../consts/granularity";
import { AssetType } from "../consts/assetType";
import { AssetName } from "../consts/assetName";

export interface useCypherBQuotesResponse {
  cypherBQuotes: CypherBQuote[];
}

export const useCypherBQuotes = (): useCypherBQuotesResponse => {
  const cypherBQuotes = useStooqStore((state) => state.cypherBQuotes);
  const isDataFetched = useRef(false);
  const fetchData = useStooqStore((state) => state.fetchCypherBQuotes);

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
          startDate: "",
          endDate: "",
        },
        waveTrendSettings: {
          channelLength: 8,
          averageLength: 6,
          movingAverageLength: 3,
          oversold: -80,
          overbought: 80,
        },
      });
    }
    fetch();
  }, [fetchData]);

  return { cypherBQuotes };
};
