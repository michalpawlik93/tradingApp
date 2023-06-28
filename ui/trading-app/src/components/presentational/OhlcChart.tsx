import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { Quote } from "../../types/Quote";
import { useMemo } from "react";

interface OhlcChartprops {
  quotes: Quote[];
}

interface ApexChartCoordinate {
  x: Date;
  y: [open: number, high: number, low: number, close: number];
}
export const OhlcChart = ({ quotes }: OhlcChartprops): JSX.Element => {
  const data: ApexChartCoordinate[] = useMemo(
    () =>
      quotes.map((quota) => ({
        x: new Date(quota.date),
        y: [quota.open, quota.high, quota.low, quota.close],
      })),
    [quotes]
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
  return quotes.length > 0 ? (
    <Chart options={options} series={series} type="candlestick"></Chart>
  ) : (
    <></>
  );
};
