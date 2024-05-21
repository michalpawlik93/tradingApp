import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { CombinedQuote } from "../../types/CombinedQuote";
import { useMemo } from "react";
import { ApexRsiChartData } from "../../types/ApexRsiChartData";
import { mapToApexRsiChartData } from "../../mappers/RsiChartMapper";
import { RsiSettings } from "../../types/RsiSettings";

interface RsiChartProps {
  combinedQuotes: CombinedQuote[];
  rsiSettings: RsiSettings;
}

export const RsiChart = ({ combinedQuotes, rsiSettings }: RsiChartProps): JSX.Element => {
  const data: ApexRsiChartData = useMemo(
    () => mapToApexRsiChartData(combinedQuotes, rsiSettings),
    [combinedQuotes, rsiSettings],
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
      animations: {
        enabled: false,
      },
    },
    title: {
      text: "RSI",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    markers: {
      size: 0,
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
