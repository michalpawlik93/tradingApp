import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import { createQuotesDataServiceMock } from "../../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { QuotesDataService } from "../../../services/QuotesDataService";
import { ChartSettingsPanelForm } from "../ChartSettingsPanelForm";

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
    const submitButton = screen.getByRole("button", { name: /submit/i });
    fireEvent.click(submitButton);
    await waitFor(() => {
      expect(QuotesDataService.getCypherB).toHaveBeenCalled();
    });
  });
});
