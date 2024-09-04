import { CypherBChart } from "../../components/presentational/Charts/CypherBChart";
import { ChartStyledPageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { useCypherBQuotes } from "../../hooks/useCypherBQuotes";
import { useTimeFrameHook } from "../../hooks/useTimeFrameHook";
import { CypherBChartForm } from "../forms/CypherBChartForm";

export const CypherBChartContiner = () => {
  const { cypherBQuotes } = useCypherBQuotes();
  const quotes = cypherBQuotes.map((x) => x.ohlc);

  const quoteDates = quotes.map((quote) => new Date(quote.date));
  const minMaxDate = useTimeFrameHook(quoteDates);

  return (
    <>
      <CypherBChartForm minMaxDate={minMaxDate} />
      <ChartStyledPageItemsWrapper>
        <CypherBChart quotes={cypherBQuotes} />
      </ChartStyledPageItemsWrapper>
    </>
  );
};
