import { SimpleChartsContainer } from "../components/containers/SimpleChartsContainer";
import { Page } from "../components/presentational/Page";

export const SimpleChartsView = () => (
  <>
    <Page
      headerProps={{
        title: "Simple Charts",
      }}
    >
      <SimpleChartsContainer />
    </Page>
  </>
);
