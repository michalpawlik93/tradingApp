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
import { RsiSettings } from "../../types/RsiSettings";
import { CombinedQuote } from "../../types/CombinedQuote";

export interface RsiChartDataProps {
  data: CombinedQuote[];
  left: string;
  right: string;
  refAreaLeft: string;
  refAreaRight: string;
  top: number;
  bottom: number;
  animation: boolean;
}

interface RsiChartProps {
  rsiSettings: RsiSettings;
  chartData: RsiChartDataProps;
  zoomOut: ()=>void;
  zoom: ()=>void;
  lineChartOnMouseDown: (e:any)=>void;
  lineChartOnMouseMove: (e:any)=>void;
}

export const RSIChart = (props:RsiChartProps): JSX.Element  => {
  return (
    <div className="highlight-bar-charts" style={{ userSelect: "none" }}>
      <button type="button" className="btn update" onClick={props.zoomOut}>
        Zoom Out
      </button>
      <LineChart
        width={800}
        height={400}
        data={props.chartData.data}
        onMouseDown={props.lineChartOnMouseDown}
        onMouseMove={props.lineChartOnMouseMove}
        onMouseUp={props.zoom}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis
          allowDataOverflow
          dataKey="ohlc.date"
          domain={[props.chartData.left, props.chartData.right]}
        />
        <YAxis
          allowDataOverflow
          domain={[props.chartData.bottom, props.chartData.top]}
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
          y={props.rsiSettings.oversold}
          stroke="red"
          strokeDasharray="3 3"
          label="Oversold"
        />
        <ReferenceLine
          yAxisId="rsiId"
          y={props.rsiSettings.overbought}
          stroke="green"
          strokeDasharray="3 3"
          label="Overbought"
        />
        {props.chartData.refAreaLeft && props.chartData.refAreaRight ? (
          <ReferenceArea
            yAxisId="rsiId"
            x1={props.chartData.refAreaLeft}
            x2={props.chartData.refAreaRight}
            strokeOpacity={0.3}
          />
        ) : null}
      </LineChart>
    </div>
  );
};
