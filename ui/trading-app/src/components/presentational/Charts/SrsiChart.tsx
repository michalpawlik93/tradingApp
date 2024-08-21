import { useMemo } from "react";
import { EChartsOption } from "echarts";
import { mapToSrsiChartData } from "../../../mappers/SrsiMapper";
import { SrsiChartData } from "../../../types/ChartData";
import { SrsiQuote } from "../../../types/SrsiQuote";
import { SRsiSettings } from "../../../types/SRsiSettings";
import { zoomOptions } from "./CommonChartOptions";
import { srsiFormatter } from "./formatters";
import { ReactEChart } from "./ReactEChart";

export const getOptions = (chartData: SrsiChartData): EChartsOption => ({
  title: {
    text: "Cypher B",
    left: 0,
  },
  ...zoomOptions,
  legend: {
    data: ["Ohlc", "Srsi %K", "Srsi %D"],
    orient: "horizontal",
    left: 300,
  },
  grid: [
    { top: "10%", left: "5%", right: "5%", height: "50%" },
    { left: "5%", right: "5%", top: "70%", height: "20%" },
  ],
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
    {
      nameLocation: "middle",
      name: "Date",
      type: "time",
      nameGap: 25,
      nameTextStyle: {
        fontSize: 14,
        fontWeight: "bold",
      },
      gridIndex: 1,
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
    {
      scale: true,
      nameLocation: "middle",
      name: "Ohlc",
      type: "value",
      nameGap: 48,
      nameTextStyle: {
        fontSize: 14,
        fontWeight: "bold",
      },
      gridIndex: 1,
    },
  ],
  tooltip: {
    trigger: "axis",
    formatter: srsiFormatter,
    axisPointer: {
      type: "cross",
      label: {
        backgroundColor: "#6a7985",
      },
    },
  },
  series: [
    {
      type: "line",
      name: "Srsi %K",
      showSymbol: false,
      color: "rgb(209,114,31)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 5,
      data: chartData.srsiStochK,
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
    {
      type: "line",
      name: "Srsi %D",
      showSymbol: false,
      color: "rgb(31,93,209)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 5,
      data: chartData.srsiStochD,
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
    {
      type: "scatter",
      name: "Srsi Sell",
      color: "rgb(222, 42, 42)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 20,
      data: chartData.srsiSell,
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
    {
      type: "scatter",
      name: "Srsi Buy",
      color: "rgb(31,93,209)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 20,
      data: chartData.srsiBuy,
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
  ],
});

export const SrsiChart = ({ quotes }: SrsiChartProps): JSX.Element => {
  const options = useMemo(() => {
    const chartData = mapToSrsiChartData(quotes);
    return getOptions(chartData);
  }, [quotes]);
  return <ReactEChart option={options} />;
};

interface SrsiChartProps {
  quotes: SrsiQuote[];
  srsiSettings: SRsiSettings;
}
