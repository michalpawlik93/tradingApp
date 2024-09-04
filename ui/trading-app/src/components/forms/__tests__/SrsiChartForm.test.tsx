import { fireEvent, render, screen, waitFor } from "@testing-library/react";
import { createQuotesDataServiceMock } from "../../../__fixtures__/QuotesDataServiceMock";
import { TestingProvider } from "../../../__fixtures__/TestingProvider";
import { QuotesDataService } from "../../../services/QuotesDataService";
import { SrsiChartForm } from "../SrsiChartForm";

describe("SrsiChartForm tests", () => {
  test("click submit button - fetch is called with form values", async () => {
    // Arrange
    vi.mocked(QuotesDataService.getCombinedQuotes).mockImplementation(
      createQuotesDataServiceMock().getCombinedQuotes,
    );
    // Act
    render(
      <TestingProvider>
        <SrsiChartForm minMaxDate={{ minDate: new Date(), maxDate: new Date() }} />
      </TestingProvider>,
    );

    // Assert
    const submitButton = screen.getByRole("button", { name: /submit/i });
    fireEvent.click(submitButton);
    await waitFor(() => {
      expect(QuotesDataService.getCombinedQuotes).toHaveBeenCalled();
    });
  });
});
