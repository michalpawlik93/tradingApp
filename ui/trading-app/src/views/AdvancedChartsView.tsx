import { Page } from "../components/presentational/Page";
import { CypherBChartContiner } from "../components/containers/CypherBChartContiner";

export const AdvancedChartsView = () => (
  <>
    <Page
      headerProps={{
        title: "Stooq Advanced Charts",
      }}
    >
      <CypherBChartContiner />
    </Page>
  </>
);
