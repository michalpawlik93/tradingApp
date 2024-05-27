import { OhlcChart } from "../presentational/OhlcChart";
import { useCypherBQuotes } from "../../hooks/useCypherBQuotes";
import { ChartStyledPageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { CypherBChart } from "../../components/presentational/CypherBChart";
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
        <OhlcChart quotes={quotes} />
      </ChartStyledPageItemsWrapper>
      <ChartStyledPageItemsWrapper>
        <CypherBChart quotes={cypherBQuotes} />
      </ChartStyledPageItemsWrapper>
    </>
  );
};
