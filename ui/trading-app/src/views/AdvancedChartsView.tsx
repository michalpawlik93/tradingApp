import { Page } from "../components/presentational/Page";
import { CypherBChartContiner } from "../components/containers/CypherBChartContiner";
import { Suspense } from "react";
import { CircularProgress } from "@mui/material";

export const AdvancedChartsView = () => {
  return (
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
};
