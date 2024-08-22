import { useMemo } from "react";
import { EChartsOption } from "echarts";
import { mapToSrsiChartData } from "../../../mappers/SrsiMapper";
import { SrsiStandaloneChartData } from "../../../types/ChartData";
import { SrsiQuote } from "../../../types/SrsiQuote";
import { zoomOptions } from "./CommonChartOptions";
import { srsiFormatter } from "./formatters";
import { ReactEChart } from "./ReactEChart";
import { SrsiBuySerie, SrsiDSerie, SrsiKSerie, SrsiSellSerie } from "./SrsiChartSeries";

export const getOptions = (chartData: SrsiStandaloneChartData): EChartsOption => ({
  title: {
    text: "Srsi",
    left: 0,
  },
  ...zoomOptions,
  legend: {
    data: ["Close", "Srsi %K", "Srsi %D"],
    orient: "horizontal",
    left: 300,
  },
  grid: [{ top: "10%", left: "5%", right: "5%" }],
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
      min: 0,
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
    SrsiKSerie(chartData),
    SrsiDSerie(chartData),
    SrsiSellSerie(chartData),
    SrsiBuySerie(chartData),
    {
      type: "line",
      name: "Close",
      color: "rgb(222, 42, 42)",
      emphasis: {
        focus: "series",
      },
      data: chartData.close,
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
}
