import { PageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { GetCombinedQuotesRequestDtoDefault } from "../../consts/defaultRequests";
import { useCombinedQuotes } from "../../hooks/useCombinedQuotes";
import { OhlcChart } from "../presentational/Charts/OhlcChart";
import { SrsiChart } from "../presentational/Charts/SrsiChart";
import { RsiChartContiner } from "./RsiChartContiner";

export const SimpleChartsContainer = () => {
  const { combinedQuotes } = useCombinedQuotes(GetCombinedQuotesRequestDtoDefault());
  const quotes = combinedQuotes.map((x) => x.ohlc);
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
    </>
  );
};
