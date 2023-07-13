import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { CypherBQuote } from "../../types/CypherBQuote";
import { useMemo } from "react";
import { ApexCypherBChartData } from "../../types/ApexCypherBChartData";
import { mapToApexChartData } from "../../mappers/CypherBChartMapper";

interface CypherBChartProps {
  quotes: CypherBQuote[];
}
//https://hackernoon.com/how-to-get-cipher-indicators-for-free-and-use-them-to-crush-the-market-yb1j359c
export const CypherBChart = ({ quotes }: CypherBChartProps): JSX.Element => {
  const chartData: ApexCypherBChartData = useMemo(
    () => mapToApexChartData(quotes),
    [quotes]
  );

  const series: ApexOptions["series"] = [
    // {
    //   name: "MFI",
    //   data: chartData.mfi,
    // },
    {
      name: "Trend Wave",
      data: chartData.waveTrend,
    },
    // {
    //   name: "VWAP",
    //   data: chartData.vwap,
    // },
  ];

  const options: ApexOptions = {
    chart: {
      height: 350,
      foreColor: "#ccc",
      animations: {
        enabled: false,
      },
    },
    markers: {
      size: 0,
    },
    title: {
      text: "Cypher B",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    fill: {
      type: "gradient",
    },
    grid: {
      borderColor: "#555",
    },
    stroke: {
      width: 1,
    },
    // colors: ["#EF3535", "#7f7fff", "#FFFF00"],
    colors: ["#7f7fff", "#FFFF00"],
    dataLabels: {
      enabled: false,
    },
    tooltip: {
      theme: "dark",
    },
    yaxis: {
      tooltip: {
        enabled: true,
      },
      // min: chartData.lowestY,
      // max: chartData.highestY,
      labels: {
        formatter: function (value) {
          return value.toFixed(4);
        },
      },
    },
  };

  return quotes.length > 0 ? (
    <Chart type="area" options={options} series={series} />
  ) : (
    <></>
  );
};
