import { OhlcChart } from "../presentational/OhlcChart";
import { useCypherBQuotes } from "../../hooks/useCypherBQuotes";
import { ChartStyledPageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { CypherBChart } from "../../components/presentational/CypherBChart";
import { useTimeFrameHook } from "../../hooks/useTimeFrameHook";
import { useEffect, useState } from "react";
import { ChartSettingsPanelForm } from "../forms/ChartSettingsPanelForm";

export const CypherBChartContiner = () => {
  const [dates, setDates] = useState<Date[]>([]);

  const { startDate, endDate, minDate, maxDate } = useTimeFrameHook(dates);
  const { cypherBQuotes } = useCypherBQuotes(startDate, endDate);
  const quotes = cypherBQuotes.map((x) => x.ohlc);

  useEffect(() => {
    if (quotes && quotes.length > 0) {
      const quoteDates = quotes.map((quote) => new Date(quote.date));
      setDates(quoteDates);
    }
  }, [quotes]);

  return (
    <>
      {quotes.length > 0 && (
        <ChartSettingsPanelForm minDate={minDate} maxDate={maxDate} />
      )}
      <ChartStyledPageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </ChartStyledPageItemsWrapper>
      <ChartStyledPageItemsWrapper>
        <CypherBChart quotes={cypherBQuotes} />
      </ChartStyledPageItemsWrapper>
    </>
  );
};
