import { useRef, useState, useEffect } from "react";
import { useStooqStore } from "../../stores/stooqStore";
import { RsiChartDataProps, RSIChart } from "../presentational/RSIChart";
import { getAxisYDomain } from "../../utils/chartUtils";

const initialState: RsiChartDataProps = {
  data: [],
  left: "dataMin",
  right: "dataMax",
  refAreaLeft: "",
  refAreaRight: "",
  top: 100,
  bottom: 0,
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
      if (!isDataFetched.current) {
        isDataFetched.current = true;
        return;
      }
      await fetchData("Daily");
    }
    fetch();
  }, [fetchData]);

  useEffect(() => {
    setChartData((prevChartData) => ({
      ...prevChartData,
      data: combinedQuotes,
    }));
  }, [combinedQuotes]);

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

    let newRefAreaLeft = refAreaLeft;
    let newRefAreaRight = refAreaRight;
    if (newRefAreaLeft > newRefAreaRight) {
      [newRefAreaLeft, newRefAreaRight] = [newRefAreaRight, newRefAreaLeft];
    }

    const [bottom, top] = getAxisYDomain(
      combinedQuotes,
      newRefAreaLeft,
      newRefAreaRight,
      "rsi",
      1
    );

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
      top: 100,
      bottom: 0,
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
