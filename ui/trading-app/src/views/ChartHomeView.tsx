import { RSIChart } from "../components/presentational/RSIChart";
import { RSIChartFunc } from "../components/presentational/RSIChartFunc";
import {quotes} from "../services/dataMock";
export const ChartHomeView = () => (
  <>
    <RSIChart quotes={quotes}/>
    <RSIChartFunc />
  </>
);
