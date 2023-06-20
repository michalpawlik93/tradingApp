import { OhlcChart } from "../presentational/OhlcChart";
import { useCypherBQuotes } from "../../hooks/useCypherBQuotes";
import { PageItemsWrapper } from "../../components/presentational/PageItemWrapper";
import { CypherBChart } from "../../components/presentational/CypherBChart";

export const CypherBChartContiner = () => {
  const { cypherBQuotes } = useCypherBQuotes();
  const quotes = cypherBQuotes.map((x) => x.ohlc);
  return (
    <>
      <PageItemsWrapper>
        <OhlcChart quotes={quotes} />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <CypherBChart quotes={cypherBQuotes} />
      </PageItemsWrapper>
    </>
  );
};
