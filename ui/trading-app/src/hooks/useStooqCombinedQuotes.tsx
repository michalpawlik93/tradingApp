
import { useRef, useEffect } from "react";
import { useStooqStore } from "../stores/stooqStore";
import { CombinedQuote } from "../types/CombinedQuote";

export interface useStooqCombinedQuotes {
    stooqCombinedQuotes: CombinedQuote[];
  }

export const useStooqCombinedQuotes = ():useStooqCombinedQuotes => {

    const stooqCombinedQuotes = useStooqStore((state) => state.combinedQuotes);
    const isDataFetched = useRef(false);
    const fetchData = useStooqStore((state) => state.fetchCombinedQuotes);

    useEffect(() => {
        async function fetch() {
          if (!isDataFetched.current) {
            isDataFetched.current = true;
            return;
          }
          await fetchData("Daily");
        }
        fetch();
      }, [fetchData]);

      return { stooqCombinedQuotes };
}