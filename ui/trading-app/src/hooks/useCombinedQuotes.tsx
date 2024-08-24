import { useEffect, useRef } from "react";
import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { useQuotesStore } from "../stores/quotesStore";
import { RsiQuote } from "../types/RsiQuote";
import { SrsiQuote } from "../types/SrsiQuote";

export interface useCombinedQuotesResponse {
  srsiQuotes: SrsiQuote[];
  rsiQuotes: RsiQuote[];
}

export const useCombinedQuotes = (
  request: GetCombinedQuotesRequestDto,
): useCombinedQuotesResponse => {
  const srsiQuotes = useQuotesStore((state) => state.srsiQuotes);
  const rsiQuotes = useQuotesStore((state) => state.rsiQuotes);
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

  return { srsiQuotes, rsiQuotes };
};
