import { LineSeriesOption, ScatterSeriesOption } from "echarts";
import { SrsiChartData } from "../../../types/ChartData";

export const SrsiKSerie = ({
  srsiStochK,
}: Pick<SrsiChartData, "srsiStochK">): LineSeriesOption => ({
  type: "line",
  name: "Srsi %K",
  showSymbol: false,
  color: "rgb(209,114,31)",
  emphasis: {
    focus: "series",
  },
  symbol: "circle",
  symbolSize: 5,
  data: srsiStochK,
  xAxisIndex: 0,
  yAxisIndex: 0,
});

export const SrsiDSerie = ({
  srsiStochD,
}: Pick<SrsiChartData, "srsiStochD">): LineSeriesOption => ({
  type: "line",
  name: "Srsi %D",
  showSymbol: false,
  color: "rgb(31,93,209)",
  emphasis: {
    focus: "series",
  },
  symbol: "circle",
  symbolSize: 5,
  data: srsiStochD,
  xAxisIndex: 0,
  yAxisIndex: 0,
});

export const SrsiSellSerie = ({
  srsiSell,
}: Pick<SrsiChartData, "srsiSell">): ScatterSeriesOption => ({
  type: "scatter",
  name: "Srsi Sell",
  color: "rgb(222, 42, 42)",
  emphasis: {
    focus: "series",
  },
  symbol: "circle",
  symbolSize: 20,
  data: srsiSell,
  xAxisIndex: 0,
  yAxisIndex: 0,
});

export const SrsiBuySerie = ({ srsiBuy }: Pick<SrsiChartData, "srsiBuy">): ScatterSeriesOption => ({
  type: "scatter",
  name: "Srsi Buy",
  color: "rgb(31,93,209)",
  emphasis: {
    focus: "series",
  },
  symbol: "circle",
  symbolSize: 20,
  data: srsiBuy,
  xAxisIndex: 0,
  yAxisIndex: 0,
});
