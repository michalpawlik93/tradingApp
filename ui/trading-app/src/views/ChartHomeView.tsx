import { RSIChartContainer } from "../components/containers/RSIChartContainer";
import {Page} from "../components/presentational/Page";
import {PageItemsWrapper} from "../components/presentational/PageItemWrapper";

export const ChartHomeView = () => (
  <>
      <Page
      headerProps={{
        title: "Data",
      }}
    >
            <PageItemsWrapper>
            <RSIChartContainer/>
            </PageItemsWrapper>
    </Page>
  </>
);
