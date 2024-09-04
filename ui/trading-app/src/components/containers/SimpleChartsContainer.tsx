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
  const { srsiQuotes, rsiQuotes } = useCombinedQuotes(GetCombinedQuotesRequestDtoDefault());
  const quotes = rsiQuotes.map((x) => x.ohlc);
  const quoteDates = quotes.map((quote) => new Date(quote.date));
  const minMaxDate = useTimeFrameHook(quoteDates);
  return (
    <>
      <PageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <RsiChartContiner rsiQuotes={rsiQuotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <SrsiChart quotes={srsiQuotes} />
      </PageItemsWrapper>

      <PageItemsWrapper>
        <BaseDialogOpener
          buttonText="Settings"
          dialogMaxWidth="lg"
          fullWidth
          closeButton
          dialogTitle="Settings Selection"
        >
          <SrsiChartForm minMaxDate={minMaxDate} />
        </BaseDialogOpener>
      </PageItemsWrapper>
    </>
  );
};
