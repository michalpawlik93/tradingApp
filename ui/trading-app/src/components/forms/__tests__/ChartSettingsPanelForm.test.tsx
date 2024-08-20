import { render, screen, fireEvent, waitFor } from "@testing-library/react";
import { ChartSettingsPanelForm } from "../ChartSettingsPanelForm";
import { QuotesDataService } from "../../../services/QuotesDataService";
import { createQuotesDataServiceMock } from "../../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { GetCypherBDtoMock } from "../../../__fixtures__/quotes";

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
      expect(QuotesDataService.getCypherB).toHaveBeenCalled();
    });
  });
});
