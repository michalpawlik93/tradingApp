import { useMemo } from "react";
import { ReactEChart } from "./ReactEChart";
import { CypherBQuote } from "../../../types/CypherBQuote";
import { CypherBChartData } from "../../../types/ChartData";
import { mapToCypherBChartData } from "../../../mappers/WaveTrendChartDataMapper";
import { EChartsOption } from "echarts";
import { zoomOptions } from "./CommonChartOptions";

const formatter = (params: any) => {
  const date = new Date(params[0].value[0]).toLocaleString();
  const wt1 = params.find((x: any) => x.seriesName === "WaveTrend WT1").value[1];
  const wt2 = params.find((x: any) => x.seriesName === "WaveTrend WT2").value[1];
  const buy = params.find((x: any) => x.seriesName === "Buy Signal")?.value[1];
  const sell = params.find((x: any) => x.seriesName === "Sell Signal")?.value[1];
  const vwap = params.find((x: any) => x.seriesName === "WaveTrend Vwap")?.value[1];
  const mfi = params.find((x: any) => x.seriesName === "Mfi")?.value[1];
  return `
    <strong>Date:</strong> ${date}<br>
    <strong>WT1:</strong> ${wt1}<br>
    <strong>WT2:</strong> ${wt2}<br>
    ${vwap !== undefined ? `<strong>Vwap</strong> ${vwap}<br>` : ""}
    ${mfi !== undefined ? `<strong>Money Flow Index</strong> ${mfi}<br>` : ""}
    ${sell !== undefined ? `<strong>Sell signal appeared</strong><br>` : ""}
    ${buy !== undefined ? `<strong>Buy signal appeared</strong>` : ""}
  `;
};

export const getOptions = (chartData: CypherBChartData): EChartsOption => ({
  title: {},
  ...zoomOptions,
  legend: {
    data: ["WaveTrend WT1", "WaveTrend WT2", "WaveTrend Vwap", "Mfi"],
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
    {
      type: "line",
      name: "WaveTrend Vwap",
      color: "rgb(137, 207, 240)",
      areaStyle: {},
      smooth: true,
      lineStyle: {
        width: 0,
      },
      showSymbol: false,
      data: chartData.waveTrendVwap,
    },
    {
      type: "line",
      name: "Mfi",
      color: "rgba(0, 255, 127, 0.2)",
      areaStyle: {},
      smooth: true,
      lineStyle: {
        width: 0,
      },
      showSymbol: false,
      data: chartData.mfi,
    },
  ],
});

export const CypherBChart = ({ quotes }: CypherBChartProps): JSX.Element => {
  const options = useMemo(() => {
    const chartData = mapToCypherBChartData(quotes);
    return getOptions(chartData);
  }, [quotes]);
  return <ReactEChart option={options} />;
};

export interface CypherBChartProps {
  quotes: CypherBQuote[];
}
