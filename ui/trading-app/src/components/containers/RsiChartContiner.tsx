import { useStooqStore } from "../../stores/stooqStore";
import { CombinedQuote } from "../../types/CombinedQuote";
import { RsiChart } from "../presentational/RsiChart";

interface RSIChartContainerProps {
  combinedQuotes: CombinedQuote[];
}
export const RsiChartContiner = ({
  combinedQuotes,
}: RSIChartContainerProps) => {
  const rsiSettings = useStooqStore((state) => state.rsiSettings);

  return <RsiChart rsiSettings={rsiSettings} combinedQuotes={combinedQuotes} />;
};
