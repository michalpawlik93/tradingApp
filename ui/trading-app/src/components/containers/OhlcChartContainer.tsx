
import { useRef, useEffect } from "react";
import { useStooqStore } from "../../stores/stooqStore";
import {OhlcChart} from "../presentational/OHLCChart";


export const OhlcChartContainer = () => {

    const combinedQuotes = useStooqStore((state) => state.combinedQuotes);
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

      return(<OhlcChart data={combinedQuotes}/>)
}