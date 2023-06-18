import Chart from "react-apexcharts";
import { ApexOptions } from "apexcharts";
import { CombinedQuote } from "../../types/CombinedQuote";
import { useMemo } from "react";
import { RsiSettings } from "../../types/RsiSettings";
import {ApexDateChartCordinate} from "../../types/ApexDateChartCordinate";

interface RsiChartProps {
  combinedQuotes: CombinedQuote[];
  rsiSettings: RsiSettings;
}

interface ApexData {
  overbought: ApexDateChartCordinate[];
  oversold: ApexDateChartCordinate[];
  rsi: ApexDateChartCordinate[];
}

export const RsiChart = ({
  combinedQuotes,
  rsiSettings,
}: RsiChartProps): JSX.Element => {
  const data: ApexData = useMemo(() => {
    const result: ApexData = {
      overbought: [],
      oversold: [],
      rsi: [],
    };
    for (let i = 0; i < combinedQuotes.length; i++) {
      const timestamp = Date.parse(combinedQuotes[i].ohlc.date);
      if (!isNaN(timestamp)) {
        const x = new Date(combinedQuotes[i].ohlc.date);
        result.overbought.push({ y: rsiSettings.overbought, x: x });
        result.oversold.push({ y: rsiSettings.oversold, x: x });
        result.rsi.push({ y: combinedQuotes[i].rsi, x: x });
      } else {
        console.log("Invalid date" + combinedQuotes[i].ohlc.date);
      }
    }
    return result;
  }, [combinedQuotes, rsiSettings]);

  const series: ApexOptions["series"] = [
    {
      name: "Overbought",
      data: data.overbought,
    },
    {
      name: "Oversold",
      data: data.oversold,
    },
    {
      name: "RSI",
      data: data.rsi,
    },
  ];
  const options: ApexOptions = {
    chart: {
      type: "line",
      height: 350,
    },
    title: {
      text: "RSI",
      align: "left",
    },
    xaxis: {
      type: "datetime",
    },
    dataLabels: {
        enabled: false
      },
    yaxis: {
      tooltip: {
        enabled: true,
      },
    },
  };
  return <Chart options={options} series={series}></Chart>;
};
