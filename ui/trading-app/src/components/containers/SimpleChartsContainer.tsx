import { OhlcChart } from "../presentational/OhlcChart";
import { useCombinedQuotes } from "../../hooks/useCombinedQuotes";
import { PageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { RsiChartContiner } from "./RsiChartContiner";

export const SimpleChartsContainer = () => {
  const { combinedQuotes } = useCombinedQuotes();
  const quotes = combinedQuotes.map((x) => x.ohlc);
  return (
    <>
      <PageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <RsiChartContiner combinedQuotes={combinedQuotes} />
      </PageItemsWrapper>
    </>
  );
};
