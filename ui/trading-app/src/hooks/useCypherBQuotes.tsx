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
        assetName: AssetName.BTC,
        assetType: AssetType.Cryptocurrency,
        granularity: Granularity.Hourly,
      });
    }
    fetch();
  }, [fetchData]);

  return { cypherBQuotes };
};
