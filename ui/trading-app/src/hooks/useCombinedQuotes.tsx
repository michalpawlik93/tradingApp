import { useEffect, useRef } from "react";
import { AssetName } from "../consts/assetName";
import { AssetType } from "../consts/assetType";
import { Granularity } from "../consts/granularity";
import { TechnicalIndicators } from "../consts/technicalIndicators";
import { useQuotesStore } from "../stores/quotesStore";
import { CombinedQuote } from "../types/CombinedQuote";

export interface useCombinedQuotesResponse {
  combinedQuotes: CombinedQuote[];
}

export const useCombinedQuotes = (): useCombinedQuotesResponse => {
  const combinedQuotes = useQuotesStore((state) => state.combinedQuotes);
  const isDataFetched = useRef(false);
  const fetchData = useQuotesStore((state) => state.fetchCombinedQuotes);

  useEffect(() => {
    async function fetch() {
      if (isDataFetched.current) {
        isDataFetched.current = true;
        return;
      }
      isDataFetched.current = true;
      await fetchData({
        asset: {
          name: AssetName.USDPLN,
          type: AssetType.Currencies,
        },
        timeFrame: {
          granularity: Granularity.FiveMins,
          startDate: new Date(2023, 5, 24).toISOString(),
          endDate: new Date(2023, 5, 28).toISOString(),
        },
        technicalIndicators: [TechnicalIndicators.Rsi],
      });
    }
    fetch();
  }, [fetchData]);

  return { combinedQuotes };
};
