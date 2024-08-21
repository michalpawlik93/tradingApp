import { CypherBChart } from "../../components/presentational/Charts/CypherBChart";
import { ChartStyledPageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { useCypherBQuotes } from "../../hooks/useCypherBQuotes";
import { useTimeFrameHook } from "../../hooks/useTimeFrameHook";
import { ChartSettingsPanelForm } from "../forms/ChartSettingsPanelForm";

export const CypherBChartContiner = () => {
  const { cypherBQuotes } = useCypherBQuotes();
  const quotes = cypherBQuotes.map((x) => x.ohlc);

  const quoteDates = quotes.map((quote) => new Date(quote.date));
  const { minDate, maxDate } = useTimeFrameHook(quoteDates);

  return (
    <>
      <ChartSettingsPanelForm minDate={minDate} maxDate={maxDate} />
      <ChartStyledPageItemsWrapper>
        <CypherBChart quotes={cypherBQuotes} />
      </ChartStyledPageItemsWrapper>
    </>
  );
};
