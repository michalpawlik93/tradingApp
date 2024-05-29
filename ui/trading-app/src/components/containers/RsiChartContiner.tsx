import { useQuotesStore } from "../../stores/quotesStore";
import { CombinedQuote } from "../../types/CombinedQuote";
import { RsiChart } from "../presentational/Charts/RsiChart";

interface RSIChartContainerProps {
  combinedQuotes: CombinedQuote[];
}
export const RsiChartContiner = ({ combinedQuotes }: RSIChartContainerProps) => {
  const rsiSettings = useQuotesStore((state) => state.rsiSettings);

  return <RsiChart rsiSettings={rsiSettings} combinedQuotes={combinedQuotes} />;
};
