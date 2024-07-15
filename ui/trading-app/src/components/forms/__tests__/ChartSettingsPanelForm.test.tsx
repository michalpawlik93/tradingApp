import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { ChartSettingsPanelForm, defaultValues } from "../ChartSettingsPanelForm";
import { QuotesDataService } from "../../../services/QuotesDataService";
import { createQuotesDataServiceMock } from "../../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import {
  mfiSettingsDefault,
  sRsiSettingsDefault,
  waveTrendSettingsDefault,
} from "../../../consts/technicalIndicatorsSettings";
import { GetCypherBDto } from "../../../services/dtos/GetCypherBDto";

describe("ChartSettingsPanelForm tests", () => {
  test("click submit button - fetch is called with form values", async () => {
    // Arrange
    vi.mocked(QuotesDataService.getCypherB).mockImplementation(
      createQuotesDataServiceMock().getCypherB,
    );
    // Act
    render(
      <TestingProvider>
        <ChartSettingsPanelForm minDate={new Date()} maxDate={new Date()} />
      </TestingProvider>,
    );

    // Assert
    const submitButton = screen.getByRole("button", { name: /Submit/i });
    fireEvent.click(submitButton);
    const expectedRequest: GetCypherBDto = {
      asset: {
        name: defaultValues.assetName,
        type: defaultValues.assetType,
      },
      sRsiSettings: sRsiSettingsDefault,
      timeFrame: {
        granularity: defaultValues.granularity,
        startDate: new Date(2023, 5, 24).toISOString(),
        endDate: new Date(2023, 5, 28).toISOString(),
      },
      waveTrendSettings: waveTrendSettingsDefault,
      mfiSettings: mfiSettingsDefault,
    };
    await waitFor(() => {
      expect(QuotesDataService.getCypherB).toHaveBeenCalledWith(expectedRequest);
    });
  });
});
