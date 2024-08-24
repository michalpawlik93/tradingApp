import { PageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { GetCombinedQuotesRequestDtoDefault } from "../../consts/defaultRequests";
import { useCombinedQuotes } from "../../hooks/useCombinedQuotes";
import { useTimeFrameHook } from "../../hooks/useTimeFrameHook";
import { SrsiChartForm } from "../forms/SrsiChartForm";
import { BaseDialogOpener } from "../presentational/BaseDialogOpener";
import { OhlcChart } from "../presentational/Charts/OhlcChart";
import { SrsiChart } from "../presentational/Charts/SrsiChart";
import { RsiChartContiner } from "./RsiChartContiner";

export const SimpleChartsContainer = () => {
  const { combinedQuotes } = useCombinedQuotes(GetCombinedQuotesRequestDtoDefault());
  const quotes = combinedQuotes.map((x) => x.ohlc);
  const quoteDates = quotes.map((quote) => new Date(quote.date));
  const { minDate, maxDate } = useTimeFrameHook(quoteDates);

  return (
    <>
      <PageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <RsiChartContiner combinedQuotes={combinedQuotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <SrsiChart quotes={combinedQuotes} />
      </PageItemsWrapper>

      <PageItemsWrapper>
        <BaseDialogOpener
          buttonText="Open"
          dialogMaxWidth="lg"
          fullWidth
          closeButton
          dialogTitle="Settings Selection"
        >
          <SrsiChartForm minDate={minDate} maxDate={maxDate} />
        </BaseDialogOpener>
      </PageItemsWrapper>
    </>
  );
};
//TO-DO: Add modal button
