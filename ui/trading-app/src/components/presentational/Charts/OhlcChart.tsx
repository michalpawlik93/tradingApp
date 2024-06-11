import { useMemo } from "react";
import { ReactEChart } from "./ReactEChart";
import { EChartsOption } from "echarts";
import { Quote } from "../../../types/Quote";
import { mapToOhlcChartData } from "../../../mappers/OhlcChartDataMapper";
import { OhlcChartData } from "../../../types/ChartData";
import { zoomOptions } from "./CommonChartOptions";
import { ohlcFormatter } from "./formatters";

const upColor = "#ec0000";
const upBorderColor = "#8A0000";
const downColor = "#00da3c";
const downBorderColor = "#008F28";

export const getOptions = (chartData: OhlcChartData): EChartsOption => ({
  title: {
    text: "OHLC Candlestick",
    left: 0,
  },
  ...zoomOptions,
  tooltip: {
    trigger: "axis",
    axisPointer: {
      type: "cross",
    },
    formatter: ohlcFormatter,
  },
  legend: {
    data: ["Ohlc"],
    orient: "horizontal",
    left: 300,
  },
  grid: {
    left: "10%",
    right: "10%",
    bottom: "15%",
  },
  xAxis: [
    {
      nameLocation: "middle",
      name: "Date",
      type: "time",
      nameGap: 25,
      nameTextStyle: {
        fontSize: 14,
        fontWeight: "bold",
      },
      gridIndex: 0,
    },
  ],
  yAxis: [
    {
      scale: true,
      nameLocation: "middle",
      name: "Value",
      type: "value",
      nameGap: 48,
      nameTextStyle: {
        fontSize: 14,
        fontWeight: "bold",
      },
      gridIndex: 0,
    },
  ],
  series: [
    {
      name: "Ohlc",
      type: "candlestick",
      data: chartData.ohlc,
      xAxisIndex: 0,
      yAxisIndex: 0,
      itemStyle: {
        color: upColor,
        color0: downColor,
        borderColor: upBorderColor,
        borderColor0: downBorderColor,
      },
    },
  ],
});

export const OhlcChart = ({ quotes }: OhlcChartProps): JSX.Element => {
  const options = useMemo(() => {
    const chartData = mapToOhlcChartData(quotes);
    return getOptions(chartData);
  }, [quotes]);
  return <ReactEChart option={options} />;
};

export interface OhlcChartProps {
  quotes: Quote[];
}
