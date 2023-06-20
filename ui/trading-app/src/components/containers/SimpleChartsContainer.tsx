import { OhlcChart } from "../presentational/OhlcChart";
import { useStooqCombinedQuotes } from "../../hooks/useStooqCombinedQuotes";
import { PageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { RsiChartContiner } from "./RsiChartContiner";

export const SimpleChartsContainer = () => {
  const { stooqCombinedQuotes } = useStooqCombinedQuotes();
  const quotes = stooqCombinedQuotes.map((x) => x.ohlc);
  return (
    <>
      <PageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <RsiChartContiner combinedQuotes={stooqCombinedQuotes} />
      </PageItemsWrapper>
    </>
  );
};
