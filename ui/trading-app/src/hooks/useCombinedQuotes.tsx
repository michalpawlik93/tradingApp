import { useEffect, useRef } from "react";
import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { useQuotesStore } from "../stores/quotesStore";
import { CombinedQuote } from "../types/CombinedQuote";

export interface useCombinedQuotesResponse {
  combinedQuotes: CombinedQuote[];
}

export const useCombinedQuotes = (
  request: GetCombinedQuotesRequestDto,
): useCombinedQuotesResponse => {
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
      await fetchData(request);
    }
    fetch();
  }, [fetchData, request]);

  return { combinedQuotes };
};
