import { useRef, useEffect } from "react";
import { useStooqStore } from "../stores/stooqStore";
import { CypherBQuote } from "../types/CypherBQuote";
import { Granularity } from "../consts/granularity";
import { AssetType } from "../consts/assetType";
import { AssetName } from "../consts/assetName";

export interface useCypherBQuotesResponse {
  cypherBQuotes: CypherBQuote[];
}

export const useCypherBQuotes = (
  startDate: Date,
  endDate: Date
): useCypherBQuotesResponse => {
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
        Granularity: Granularity.FiveMins,
        AssetType: AssetType.Cryptocurrency,
        AssetName: AssetName.ANC,
        StartDate: startDate.toISOString(),
        EndDate: endDate.toISOString(),
      });
    }
    fetch();
  }, [fetchData, startDate, endDate]);

  return { cypherBQuotes };
};
