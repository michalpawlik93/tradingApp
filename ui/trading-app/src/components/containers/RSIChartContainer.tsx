import { useRef, useState, useEffect } from "react";
import { useStooqStore } from "../../stores/stooqStore";
import { RsiChartDataProps, RSIChart } from "../presentational/RSIChart";
import {CombinedQuote} from "../../types/CombinedQuote";

const getAxisYDomain = (
  initialData: CombinedQuote[] | undefined,
  from: string,
  to: string,
  ref: keyof CombinedQuote,
  offset: number
) => {
  if (!initialData || initialData.length === 0) {
    return [0, 100];
  }
  const fromDate = new Date(from);
  const toDate = new Date(to);

  if (isNaN(fromDate.getTime()) || isNaN(toDate.getTime())) {
    return [0, 100];
  }
  const refData = initialData.filter((d) => {
    const currentDate = new Date(d.ohlc.date);
    return currentDate >= fromDate && currentDate <= toDate;
  });

  if (refData.length === 0) {
    return [0, 100];
  }

  let [bottom, top] = [refData[0][ref], refData[refData.length-1][ref]];
  refData.forEach((d) => {
    if (d[ref] > top) top = d[ref];
    if (d[ref] < bottom) bottom = d[ref];
  });

  return[(bottom as number || 0) - offset, (top as number || 0) + offset];
};

const initialState: RsiChartDataProps = {
  data: [],
  left: "dataMin",
  right: "dataMax",
  refAreaLeft: "",
  refAreaRight: "",
  top: 120,
  bottom: -20,
  animation: true,
};

export const RSIChartContainer = () => {
  const [chartData, setChartData] = useState<RsiChartDataProps>(initialState);
  const rsiSettings = useStooqStore((state) => state.rsiSettings);
  const fetchData = useStooqStore((state) => state.fetchCombinedQuotes);
  const combinedQuotes = useStooqStore((state) => state.combinedQuotes);
  const isDataFetched = useRef(false);

  useEffect(() => {
    async function fetch() {
      await fetchData();
      setChartData((prevChartData) => ({
        ...prevChartData,
        data: combinedQuotes,
      }));
      isDataFetched.current = true;
    }
    if (!isDataFetched.current) {
      fetch();
    }
  }, [combinedQuotes, fetchData, chartData]);

  const zoom = () => {
    const { refAreaLeft, refAreaRight } = chartData;
    const { data } = chartData;

    if (refAreaLeft === refAreaRight || refAreaRight === "") {
      setChartData((prevChartData) => ({
        ...prevChartData,
        refAreaLeft: "",
        refAreaRight: "",
      }));
      return;
    }

    // xAxis domain
    let newRefAreaLeft = refAreaLeft;
    let newRefAreaRight = refAreaRight;
    if (newRefAreaLeft > newRefAreaRight) {
      [newRefAreaLeft, newRefAreaRight] = [newRefAreaRight, newRefAreaLeft];
    }

    // yAxis domain
    const [bottom, top] = getAxisYDomain(
      combinedQuotes,
      newRefAreaLeft,
      newRefAreaRight,
      "rsi",
      1
    );

    console.log(newRefAreaLeft)
    console.log(newRefAreaRight)
    console.log(bottom)
    console.log(top)
    setChartData((prevChartData) => ({
      ...prevChartData,
      refAreaLeft: "",
      refAreaRight: "",
      data: data.slice(),
      left: newRefAreaLeft,
      right: newRefAreaRight,
      bottom: bottom,
      top: top,
    }));
  };

  const zoomOut = () => {
    setChartData((prevChartData) => ({
      ...prevChartData,
      data: combinedQuotes,
      refAreaLeft: "",
      refAreaRight: "",
      left: "dataMin",
      right: "dataMax",
      top: 120,
      bottom: -20,
    }));
  };

  const lineChartOnMouseMove = (e: any) =>
    chartData.refAreaLeft &&
    setChartData((prevChartData) => ({
      ...prevChartData,
      refAreaRight: e.activeLabel,
    }));

  const lineChartOnMouseDown = (e: any) =>
    setChartData((prevChartData) => ({
      ...prevChartData,
      refAreaLeft: e.activeLabel,
    }));
    
  return (
    <RSIChart
      rsiSettings={rsiSettings}
      chartData={chartData}
      zoomOut={zoomOut}
      zoom={zoom}
      lineChartOnMouseDown={lineChartOnMouseDown}
      lineChartOnMouseMove={lineChartOnMouseMove}
    />
  );
};
