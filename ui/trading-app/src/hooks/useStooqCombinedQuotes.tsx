import { useRef, useEffect } from "react";
import { useStooqStore } from "../stores/stooqStore";
import { CombinedQuote } from "../types/CombinedQuote";
import { Granularity } from "../consts/granularity";
import { AssetType } from "../consts/assetType";
import { AssetName } from "../consts/assetName";

export interface useStooqCombinedQuotes {
  stooqCombinedQuotes: CombinedQuote[];
}

export const useStooqCombinedQuotes = (): useStooqCombinedQuotes => {
  const stooqCombinedQuotes = useStooqStore((state) => state.combinedQuotes);
  const isDataFetched = useRef(false);
  const fetchData = useStooqStore((state) => state.fetchCombinedQuotes);

  useEffect(() => {
    async function fetch() {
      if (!isDataFetched.current) {
        isDataFetched.current = true;
        return;
      }
      await fetchData({
        granularity: Granularity.FiveMins,
        assetType: AssetType.Cryptocurrency,
        assetName: AssetName.ANC,
        startDate: new Date(2023, 5, 24).toISOString(),
        endDate: new Date(2023, 5, 28).toISOString(),
      });
    }
    fetch();
  }, [fetchData]);

  return { stooqCombinedQuotes };
};
