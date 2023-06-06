import { useRef, useState, useEffect } from "react";
import {
  LineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  ReferenceArea,
  ReferenceLine,
} from "recharts";
import { useStooqStore } from "../../stores/stooqStore";
import { CombinedQuote } from "../../types/CombinedQuote";

const getAxisYDomain = (
  initialData: any,
  from: number,
  to: number,
  ref: string,
  offset: number
) => {
  if (!initialData) {
    return [0, 100];
  }
  const refData = initialData.slice(from - 1, to);
  let [bottom, top] = [refData[0][ref], refData[0][ref]];

  refData.forEach((d) => {
    if (d[ref] > top) top = d[ref];
    if (d[ref] < bottom) bottom = d[ref];
  });

  return [(bottom | 0) - offset, (top | 0) + offset];
};

const initialState: RsiChartProps = {
  data: [],
  left: "dataMin",
  right: "dataMax",
  refAreaLeft: "",
  refAreaRight: "",
  top: "120",
  bottom: "-20",
  animation: true,
};

interface RsiChartProps {
  data: CombinedQuote[];
  left: string;
  right: string;
  refAreaLeft: string;
  refAreaRight: string;
  top: string;
  bottom: string;
  animation: boolean;
}

export const RSIChartFunc = () => {
  const [chartData, setChartData] = useState<RsiChartProps>(initialState);
  const rsiSettings = useStooqStore((state) => state.rsiSettings);
  const fetchData = useStooqStore((state) => state.fetchData);
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
    if(!isDataFetched.current){
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
    let newRefAreaLeft = parseFloat(refAreaLeft);
    let newRefAreaRight = parseFloat(refAreaRight);
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
    setChartData((prevChartData) => ({
      ...prevChartData,
      refAreaLeft: "",
      refAreaRight: "",
      data: data.slice(),
      left: newRefAreaLeft.toString(),
      right: newRefAreaRight.toString(),
      bottom: bottom.toString(),
      top: top.toString(),
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
      top: "dataMax+1",
      bottom: "dataMin-1",
    }));
  };

  return (
    <div className="highlight-bar-charts" style={{ userSelect: "none" }}>
      <button type="button" className="btn update" onClick={zoomOut}>
        Zoom Out
      </button>
      <LineChart
        width={800}
        height={400}
        data={chartData.data}
        onMouseDown={(e: any) =>
          setChartData((prevChartData) => ({
            ...prevChartData,
            refAreaLeft: e.activeLabel,
          }))
        }
        onMouseMove={(e: any) =>
          chartData.refAreaLeft &&
          setChartData((prevChartData) => ({
            ...prevChartData,
            refAreaRight: e.activeLabel,
          }))
        }
        onMouseUp={zoom}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis
          allowDataOverflow
          dataKey="ohlc.date"
          domain={[chartData.left, chartData.right]}
        />
        <YAxis
          allowDataOverflow
          domain={[chartData.bottom, chartData.top]}
          type="number"
          yAxisId="rsiId"
        />
        <Tooltip />
        <Line
          yAxisId="rsiId"
          type="natural"
          dataKey="rsi"
          stroke="#8884d8"
          animationDuration={300}
        />
        <ReferenceLine
          yAxisId="rsiId"
          y={rsiSettings.oversold}
          stroke="red"
          strokeDasharray="3 3"
          label="Oversold"
        />
        <ReferenceLine
          yAxisId="rsiId"
          y={rsiSettings.overbought}
          stroke="green"
          strokeDasharray="3 3"
          label="Overbought"
        />
        {chartData.refAreaLeft && chartData.refAreaRight ? (
          <ReferenceArea
            yAxisId="rsiId"
            x1={chartData.refAreaLeft}
            x2={chartData.refAreaRight}
            strokeOpacity={0.3}
          />
        ) : null}
      </LineChart>
    </div>
  );
};
