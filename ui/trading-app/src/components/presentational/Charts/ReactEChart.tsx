import { useEffect, useRef } from "react";
import { ECharts, EChartsOption, getInstanceByDom, init } from "echarts";

export interface ReactEChartProps {
  option: EChartsOption;
}

export function ReactEChart({ option }: ReactEChartProps): JSX.Element {
  const chartRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    let chart: ECharts | undefined;
    if (chartRef.current !== null) {
      chart = init(chartRef.current, null, {
        height: 1000,
      });
    }

    function resizeChart() {
      chart?.resize();
    }
    window.addEventListener("resize", resizeChart);
    return () => {
      chart?.dispose();
      window.removeEventListener("resize", resizeChart);
    };
  }, []);

  useEffect(() => {
    if (chartRef.current !== null) {
      const chart = getInstanceByDom(chartRef.current);
      if (chart) {
        chart.setOption(option);
      }
    }
  }, [option]);

  return (
    <div ref={chartRef} style={{ width: "100%", height: "100%" }} data-testid="echarts-react" />
  );
}
