import { OhlcChart } from "../presentational/OhlcChart";
import { useCypherBQuotes } from "../../hooks/useCypherBQuotes";
import { ChartStyledPageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { CypherBChart } from "../../components/presentational/CypherBChart";

export const CypherBChartContiner = () => {
  const { cypherBQuotes } = useCypherBQuotes();
  const quotes = cypherBQuotes.map((x) => x.ohlc);
  return (
    <>
      <ChartStyledPageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </ChartStyledPageItemsWrapper>
      <ChartStyledPageItemsWrapper>
        <CypherBChart quotes={cypherBQuotes} />
      </ChartStyledPageItemsWrapper>
    </>
  );
};
