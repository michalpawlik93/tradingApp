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
  const data: ApexCypherBChartData = useMemo(
    () => mapToApexChartData(quotes),
    [quotes]
  );

  const series: ApexOptions["series"] = [
    {
      name: "MFI",
      data: data.mfi,
    },
    {
      name: "Momentum Wave",
      data: data.momentumWave,
    },
    {
      name: "VWAP",
      data: data.vwap,
    },
  ];
  const options: ApexOptions = {
    chart: {
      type: "line",
      height: 350,
    },
    title: {
      text: "Cypher B",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    dataLabels: {
      enabled: false,
    },
    yaxis: {
      tooltip: {
        enabled: true,
      },
    },
  };
  return <Chart options={options} series={series}></Chart>;
};
