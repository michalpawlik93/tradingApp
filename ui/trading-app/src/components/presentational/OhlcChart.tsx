import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { CombinedQuote } from "../../types/CombinedQuote";
import { useMemo } from "react";

interface OhlcChartprops {
  combinedQuotes: CombinedQuote[];
}

interface ApexChartCoordinate {
  x: Date;
  y: [open: number, high: number, low: number, close: number];
}
export const OhlcChart = ({ combinedQuotes }: OhlcChartprops): JSX.Element => {
  const data: ApexChartCoordinate[] = useMemo(
    () =>
      combinedQuotes.map((quota) => ({
        x: new Date(quota.ohlc.date),
        y: [quota.ohlc.open, quota.ohlc.high, quota.ohlc.low, quota.ohlc.close],
      })),
    [combinedQuotes]
  );

  const series: ApexOptions["series"] = [
    {
      data: data,
    },
  ];
  const options: ApexOptions = {
    chart: {
      type: "candlestick",
      height: 350,
    },
    title: {
      text: "OHLC",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    yaxis: {
      tooltip: {
        enabled: true,
      },
    },
  };
  return <Chart options={options} series={series} type= "candlestick"></Chart>;
};
