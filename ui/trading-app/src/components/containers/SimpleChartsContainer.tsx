import { OhlcChart } from "../presentational/OhlcChart";
import { useStooqCombinedQuotes } from "../../hooks/useStooqCombinedQuotes";
import { PageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { RsiChartContiner } from "./RsiChartContiner";

export const SimpleChartsContainer = () => {
  const { stooqCombinedQuotes } = useStooqCombinedQuotes();
  return (
    <>
      <PageItemsWrapper>
        <OhlcChart combinedQuotes={stooqCombinedQuotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <RsiChartContiner combinedQuotes={stooqCombinedQuotes} />
      </PageItemsWrapper>
    </>
  );
};
