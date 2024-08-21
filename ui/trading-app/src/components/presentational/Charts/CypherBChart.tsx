import { useMemo } from "react";
import { EChartsOption } from "echarts";
import { waveTrendSettingsDefault } from "../../../consts/technicalIndicatorsSettings";
import { mapToCypherBChartData } from "../../../mappers/CypherBChartDataMapper";
import { CypherBChartData } from "../../../types/ChartData";
import { CypherBQuote } from "../../../types/CypherBQuote";
import { zoomOptions } from "./CommonChartOptions";
import { cypherBFormatter } from "./formatters";
import { ReactEChart } from "./ReactEChart";

const upColor = "#ec0000";
const upBorderColor = "#8A0000";
const downColor = "#00da3c";
const downBorderColor = "#008F28";
export const getOptions = (chartData: CypherBChartData): EChartsOption => ({
  title: {
    text: "Cypher B",
    left: 0,
  },
  ...zoomOptions,
  legend: {
    data: ["WaveTrend WT1", "WaveTrend WT2", "WaveTrend Vwap", "Mfi Buy", "Mfi Sell", "Ohlc"],
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
    formatter: cypherBFormatter,
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
            yAxis: waveTrendSettingsDefault.overbought,
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
            yAxis: waveTrendSettingsDefault.overboughtLevel2,
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
            yAxis: waveTrendSettingsDefault.oversold,
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
            yAxis: waveTrendSettingsDefault.oversoldLevel2,
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
      xAxisIndex: 0,
      yAxisIndex: 0,
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
      xAxisIndex: 0,
      yAxisIndex: 0,
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
      data: chartData.waveTrendSell,
      xAxisIndex: 0,
      yAxisIndex: 0,
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
      data: chartData.waveTrendBuy,
      xAxisIndex: 0,
      yAxisIndex: 0,
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
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
    {
      type: "line",
      name: "Mfi Buy",
      color: "rgba(0, 255, 127, 0.2)",
      areaStyle: {},
      smooth: true,
      lineStyle: {
        width: 0,
      },
      showSymbol: false,
      data: chartData.mfiBuy,
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
    {
      type: "line",
      name: "Mfi Sell",
      color: "rgba(255, 0, 0, 0.2)",
      areaStyle: {},
      smooth: true,
      lineStyle: {
        width: 0,
      },
      showSymbol: false,
      data: chartData.mfiSell,
      xAxisIndex: 0,
      yAxisIndex: 0,
    },
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
    {
      name: "Ohlc",
      type: "candlestick",
      data: chartData.ohlc,
      xAxisIndex: 1,
      yAxisIndex: 1,
      itemStyle: {
        color: downColor,
        color0: upColor,
        borderColor: downBorderColor,
        borderColor0: upBorderColor,
      },
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
