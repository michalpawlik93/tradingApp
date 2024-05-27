import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { ChartSettingsPanelForm, defaultValues } from "../ChartSettingsPanelForm";
import { QuotesDataService } from "../../../services/QuotesDataService";
import { createQuotesDataServiceMock } from "../../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";

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
    await waitFor(() => {
      expect(QuotesDataService.getCypherB).toHaveBeenCalledWith({
        asset: {
          name: defaultValues.assetName,
          type: defaultValues.assetType,
        },
        sRsiSettings: {
          enable: false,
          length: 0,
          stochDSmooth: 0,
          stochKSmooth: 0,
        },
        timeFrame: {
          granularity: defaultValues.granularity,
        },
        waveTrendSettings: {
          averageLength: 0,
          channelLength: 0,
          movingAverageLength: 0,
          overbought: 0,
          oversold: 0,
        },
      });
    });
  });
});
