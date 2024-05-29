import { useMemo } from "react";
import { ReactEChart } from "./ReactEChart";
import { CypherBQuote } from "../../../types/CypherBQuote";
import { WaveTrendChartData } from "../../../types/ChartData";
import { mapToWaveTrendChartData } from "../../../mappers/WaveTrendChartDataMapper";
import { EChartsOption } from "echarts";
import { zoomOptions } from "./CommonChartOptions";

const formatter = (params: any) => {
  const date = new Date(params[0].value[0]).toLocaleString();
  const wt1 = params[0].value[1];
  const wt2 = params[1].value[1];
  const sell = params[2]?.value[1];
  const buy = params[3]?.value[1];

  return `
    <strong>Date:</strong> ${date}<br>
    <strong>WT1:</strong> ${wt1}<br>
    <strong>WT2:</strong> ${wt2}<br>
    ${sell !== undefined ? `<strong>Sell signal appeared</strong><br>` : ""}
    ${buy !== undefined ? `<strong>Buy signal appeared</strong>` : ""}
  `;
};

export const getOptions = (chartData: WaveTrendChartData): EChartsOption => ({
  title: {},
  ...zoomOptions,
  legend: {
    data: ["WaveTrend WT1", "WaveTrend WT2"],
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
      name: "WaveTrend WT1",
      color: "rgb(97,184,119)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 5,
      data: chartData.waveTrendWt1,
      markLine: {
        data: [
          {
            yAxis: 60,
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
            yAxis: 53,
            lineStyle: { color: "#e3004d" },
            label: {
              show: true,
              position: "end",
              formatter: "Overbought Level 2",
              color: "#fff",
              backgroundColor: "#e3004d",
            },
          },
          {
            yAxis: -60,
            lineStyle: { color: "#00E396" },
            label: {
              show: true,
              position: "end",
              formatter: "Oversold",
              color: "#fff",
              backgroundColor: "#00E396",
            },
          },
          {
            yAxis: -53,
            lineStyle: { color: "#00E396" },
            label: {
              show: true,
              position: "end",
              formatter: "Oversold Level 2",
              color: "#fff",
              backgroundColor: "#00E396",
            },
          },
        ],
      },
    },
    {
      type: "scatter",
      name: "WaveTrend WT2",
      color: "rgb(184,97,119)",
      emphasis: {
        focus: "series",
      },
      symbol: "circle",
      symbolSize: 5,
      data: chartData.waveTrendWt2,
    },
    {
      type: "scatter",
      name: "Sell Signal",
      color: "rgb(184,97,119)",
      emphasis: {
        focus: "series",
      },
      symbol: "square",
      symbolSize: 20,
      data: chartData.sellSignals,
    },
    {
      type: "scatter",
      name: "Buy Signal",
      color: "rgb(97,184,119)",
      emphasis: {
        focus: "series",
      },
      symbol: "square",
      symbolSize: 20,
      data: chartData.buySignals,
    },
  ],
});

export const CypherBChart = ({ quotes }: CypherBChartProps): JSX.Element => {
  const options = useMemo(() => {
    const chartData = mapToWaveTrendChartData(quotes);
    return getOptions(chartData);
  }, [quotes]);
  return <ReactEChart option={options} />;
};

export interface CypherBChartProps {
  quotes: CypherBQuote[];
}
