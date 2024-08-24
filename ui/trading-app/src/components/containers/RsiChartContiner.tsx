import { useQuotesStore } from "../../stores/quotesStore";
import { RsiQuote } from "../../types/RsiQuote";
import { RsiChart } from "../presentational/Charts/RsiChart";

interface RSIChartContainerProps {
  rsiQuotes: RsiQuote[];
}
export const RsiChartContiner = ({ rsiQuotes }: RSIChartContainerProps) => {
  const rsiSettings = useQuotesStore((state) => state.rsiSettings);

  return <RsiChart rsiSettings={rsiSettings} rsiQuotes={rsiQuotes} />;
};
