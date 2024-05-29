import { useMemo } from "react";
import { ReactEChart } from "./ReactEChart";
import { EChartsOption } from "echarts";
import { Quote } from "../../../types/Quote";
import { mapToOhlcChartData } from "../../../mappers/OhlcChartDataMapper";
import { OhlcChartData } from "../../../types/ChartData";
import { zoomOptions } from "./CommonChartOptions";

const upColor = "#ec0000";
const upBorderColor = "#8A0000";
const downColor = "#00da3c";
const downBorderColor = "#008F28";

export const getOptions = (chartData: OhlcChartData): EChartsOption => ({
  title: {
    text: "OHLC Chart",
    left: 0,
  },
  ...zoomOptions,
  tooltip: {
    trigger: "axis",
    axisPointer: {
      type: "cross",
    },
    formatter: (params: any) => {
      const date = new Date(params[0].value[0]).toLocaleString();
      return `
        <strong>Date:</strong> ${date}<br>
        <strong>Open:</strong> ${params[0].value[1]}<br>
        <strong>Close:</strong> ${params[0].value[2]}<br>
        <strong>Low:</strong> ${params[0].value[3]}<br>
        <strong>High:</strong> ${params[0].value[4]}<br>
      `;
    },
  },
  legend: {
    data: ["Candlestick"],
  },
  grid: {
    left: "10%",
    right: "10%",
    bottom: "15%",
  },
  xAxis: {
    type: "category",
    data: chartData.categoryData,
    boundaryGap: true,
  },
  yAxis: {
    scale: true,
  },
  series: [
    {
      name: "Candlestick",
      type: "candlestick",
      data: chartData.values,
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
