import { EChartsOption } from "echarts";

export const zoomOptions: Partial<EChartsOption> = {
  toolbox: {
    feature: {
      dataZoom: {
        yAxisIndex: "none",
      },
      restore: {},
      saveAsImage: {},
    },
  },
  dataZoom: [
    {
      show: true,
      realtime: true,
      start: 0,
      end: 100,
      xAxisIndex: [0, 1],
    },
    {
      type: "inside",
      realtime: true,
      start: 0,
      end: 100,
      xAxisIndex: [0, 1],
    },
  ],
};
