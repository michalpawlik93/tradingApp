import { RSIChart } from "../components/presentational/RSIChart";
import {quotes} from "../services/dataMock";
export const ChartHomeView = () => (
  <>
    <RSIChart quotes={quotes}/>
  </>
);
