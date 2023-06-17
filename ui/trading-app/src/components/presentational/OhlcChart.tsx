import Chart from "react-apexcharts";
import {ApexOptions} from "apexcharts";
import { CombinedQuote } from "../../types/CombinedQuote";

export interface OhlcChartprops {
  data: CombinedQuote[];
}

interface ApexChartCoordinate {
  x: Date;
  y: [open: number, high: number, low: number, close: number];
}
export const OhlcChart = (props: OhlcChartprops): JSX.Element => {
  const data: ApexChartCoordinate[] = props.data.map((quota) => {
    return {
      x: new Date(quota.ohlc.date),
      y: [quota.ohlc.open, quota.ohlc.high, quota.ohlc.low, quota.ohlc.close],
    };
  });

  const series:ApexOptions['series'] = [{
    data:data
  }]
  const options: ApexOptions = {
    chart: {
      type:"candlestick",
      height: 350,
    },
    title: {
      text: "CandleStick Chart",
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
  return (
    <div>
      <Chart options={options} series={series} type="candlestick"></Chart>
    </div>
  );
};
