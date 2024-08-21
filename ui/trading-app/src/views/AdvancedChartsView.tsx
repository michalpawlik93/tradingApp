import { Suspense } from "react";
import { CircularProgress } from "@mui/material";
import { CypherBChartContiner } from "../components/containers/CypherBChartContiner";
import { Page } from "../components/presentational/Page";

export const AdvancedChartsView = () => (
  <Page
    headerProps={{
      title: "Advanced Charts",
    }}
  >
    <Suspense fallback={<CircularProgress />}>
      <CypherBChartContiner />
    </Suspense>
  </Page>
);
