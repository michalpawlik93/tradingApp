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

  // const waveTrendPoints = useMemo(() => {
  //   const a = chartData.waveTrend
  //     .filter(
  //       (cordinate: WaveTrendChartCordinate) =>
  //         cordinate.crossesOver || cordinate.crossesUnder
  //     )
  //     .map((cordinate: WaveTrendChartCordinate) => {
  //       return {
  //         x: cordinate.x.toDateString(),
  //         y: cordinate.y,
  //         marker: {
  //           size: 16,
  //           fillColor: cordinate.crossesOver ? "#E83810" : "#E83810",
  //           strokeColor: "#E83810",
  //           radius: 2,
  //         },
  //         label: {
  //           borderColor: "#FF4560",
  //           offsetY: 0,
  //           style: {
  //             color: "#fff",
  //             background: "#FF4560",
  //           },

  //           text: "Point Annotation (XY)",
  //         },
  //       };
  //     });
  //   console.log(a);
  //   return a;
  // }, [chartData.waveTrend]);

  const series: ApexOptions["series"] = [
    // {
    //   name: "MFI",
    //   data: chartData.mfi,
    // },
    // {
    //   name: "Trend Wave",
    //   data: chartData.waveTrend,
    // },
    {
      name: "VWAP",
      data: chartData.vwap,
    },
  ];

  const options: ApexOptions = {
    annotations: {
      yaxis: [
        {
          y: 80,
          borderColor: "#00E396",
          label: {
            borderColor: "#00E396",
            style: {
              color: "#fff",
              background: "#00E396",
            },
            text: "Overbought",
          },
        },
        {
          y: -80,
          borderColor: "#00E396",
          label: {
            borderColor: "#00E396",
            style: {
              color: "#fff",
              background: "#00E396",
            },
            text: "Oversold",
          },
        },
      ],
      points: [
        // {
        //   x: new Date("27 Oct 2022").getTime(),
        //   y: 80,
        //   marker: {
        //     size: 6,
        //     fillColor: "#fff",
        //     strokeColor: "#2698FF",
        //     radius: 2,
        //   },
        //   label: {
        //     borderColor: "#FF4560",
        //     offsetY: 0,
        //     style: {
        //       color: "#fff",
        //       background: "#FF4560",
        //     },
        //     text: "ssssss",
        //   },
        // },
      ],
    },
    chart: {
      animations: {
        enabled: false,
      },
      height: 350,
      foreColor: "#ccc",
    },
    title: {
      text: "Cypher B",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    grid: {
      borderColor: "#555",
    },
    stroke: {
      width: 3,
    },
    // colors: ["#EF3535", "#7f7fff", "#FFFF00"],
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
