import { useRef, useEffect } from "react";
import { useQuotesStore } from "../stores/quotesStore";
import { CombinedQuote } from "../types/CombinedQuote";
import { Granularity } from "../consts/granularity";
import { AssetType } from "../consts/assetType";
import { AssetName } from "../consts/assetName";
import { TechnicalIndicators } from "../consts/technicalIndicators";

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
        granularity: Granularity.FiveMins,
        assetType: AssetType.Currencies,
        assetName: AssetName.USDPLN,
        startDate: new Date(2023, 5, 24).toISOString(),
        endDate: new Date(2023, 5, 28).toISOString(),
        technicalIndicators: [TechnicalIndicators.Rsi],
      });
    }
    fetch();
  }, [fetchData]);

  return { combinedQuotes };
};
