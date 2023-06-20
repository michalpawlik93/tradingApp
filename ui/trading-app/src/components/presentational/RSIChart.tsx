import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { CombinedQuote } from "../../types/CombinedQuote";
import { useMemo } from "react";
import { RsiSettings } from "../../types/RsiSettings";
import { ApexRsiChartData } from "../../types/ApexRsiChartData";
import { mapToApexRsiChartData } from "../../mappers/RsiChartMapper";

interface RsiChartProps {
  combinedQuotes: CombinedQuote[];
  rsiSettings: RsiSettings;
}

export const RsiChart = ({
  combinedQuotes,
  rsiSettings,
}: RsiChartProps): JSX.Element => {
  const data: ApexRsiChartData = useMemo(
    () => mapToApexRsiChartData(combinedQuotes, rsiSettings),
    [combinedQuotes, rsiSettings]
  );

  const series: ApexOptions["series"] = [
    {
      name: "Overbought",
      data: data.overbought,
    },
    {
      name: "Oversold",
      data: data.oversold,
    },
    {
      name: "RSI",
      data: data.rsi,
    },
  ];
  const options: ApexOptions = {
    chart: {
      type: "line",
      height: 350,
    },
    title: {
      text: "RSI",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    dataLabels: {
      enabled: false,
    },
    yaxis: {
      tooltip: {
        enabled: true,
      },
    },
  };
  return <Chart options={options} series={series}></Chart>;
};
