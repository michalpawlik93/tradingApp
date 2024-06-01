import { useMemo } from "react";
import { ReactEChart } from "./ReactEChart";
import { RsiChartData } from "../../../types/ChartData";
import { EChartsOption } from "echarts";
import { mapToRsiChartData } from "../../../mappers/RsiChartMapper";
import { CombinedQuote } from "../../../types/CombinedQuote";
import { RsiSettings } from "../../../types/RsiSettings";
import { zoomOptions } from "./CommonChartOptions";

const formatter = (params: any) => {
  const date = new Date(params[0].value[0]).toLocaleString();
  const rsi = params[0].value[1];

  return `
    <strong>Date:</strong> ${date}<br>
    <strong>WT1:</strong> ${rsi}<br>
  `;
};

export const getOptions = (chartData: RsiChartData): EChartsOption => ({
  title: {},
  ...zoomOptions,
  legend: {
    data: ["RSI"],
    orient: "horizontal",
    left: 0,
  },
  xAxis: {
    nameLocation: "middle",
    name: "Date",
    type: "time",
    nameGap: 25,
    nameTextStyle: {
      fontSize: 14,
      fontWeight: "bold",
    },
  },
  yAxis: {
    scale: true,
    nameLocation: "middle",
    name: "Value",
    type: "value",
    nameGap: 48,
    nameTextStyle: {
      fontSize: 14,
      fontWeight: "bold",
    },
  },
  tooltip: {
    trigger: "axis",
    formatter,
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
      name: "Rsi",
      color: "rgb(97,184,119)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 5,
      data: chartData.rsi,
      markLine: {
        data: [
          {
            yAxis: chartData.overbought,
            lineStyle: { color: "#e3004d" },
            label: {
              show: true,
              position: "end",
              formatter: "Overbought",
              color: "#fff",
              backgroundColor: "#e3004d",
            },
          },
          {
            yAxis: chartData.oversold,
            lineStyle: { color: "#00E396" },
            label: {
              show: true,
              position: "end",
              formatter: "Oversold",
              color: "#fff",
              backgroundColor: "#00E396",
            },
          },
        ],
      },
    },
  ],
});

export const RsiChart = ({ combinedQuotes, rsiSettings }: RsiChartProps): JSX.Element => {
  const options = useMemo(() => {
    const chartData = mapToRsiChartData(combinedQuotes, rsiSettings);
    return getOptions(chartData);
  }, [combinedQuotes, rsiSettings]);
  return <ReactEChart option={options} />;
};

interface RsiChartProps {
  combinedQuotes: CombinedQuote[];
  rsiSettings: RsiSettings;
}
