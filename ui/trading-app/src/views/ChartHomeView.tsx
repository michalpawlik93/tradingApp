import { RSIChartContainer } from "../components/containers/RSIChartContainer";
import { OhlcChartContainer } from "../components/containers/OhlcChartContainer";
import { Page } from "../components/presentational/Page";
import { PageItemsWrapper } from "../components/presentational/PageItemWrapper";

export const ChartHomeView = () => (
  <>
    <Page
      headerProps={{
        title: "Charts",
      }}
    >
      <PageItemsWrapper>
        <OhlcChartContainer />
      </PageItemsWrapper>
      <PageItemsWrapper>
        <RSIChartContainer />
      </PageItemsWrapper>
    </Page>
  </>
);
