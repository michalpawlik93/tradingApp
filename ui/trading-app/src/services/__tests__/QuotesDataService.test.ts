import {
  GetCombinedQuotesResponseDtoMock,
  GetCypherBDtoMock,
  GetCypherBResponseDtoMock,
  GetQuotesRequestDtoMock,
} from "src/__fixtures__/quotes";
import { QuotesDataService } from "../QuotesDataService";
import { GetCombinedQuotesResponseDto } from "../dtos/GetCombinedQuotesResponseDto";
import { GetCypherBResponseDto } from "../dtos/GetCypherBResponseDto";
import { StooqUrls } from "../urls/stooqUrl";
import { Mock } from "vitest";

vi.unmock("../QuotesDataService");
describe("QuotesDataService", () => {
  test("getCombinedQuotes", async () => {
    // Arrange
    const request = GetQuotesRequestDtoMock();
    const expectedResponse: GetCombinedQuotesResponseDto = GetCombinedQuotesResponseDtoMock();
    const mockedImplementation = () =>
      Promise.resolve({
        json() {
          return { data: expectedResponse };
        },
      });
    global.fetch = vi.fn(mockedImplementation) as Mock;

    // Act
    const response = await QuotesDataService.getCombinedQuotes(GetQuotesRequestDtoMock());

    // Assert
    expect(fetch).toHaveBeenCalledTimes(1);
    expect(fetch).toHaveBeenCalledWith(StooqUrls.combinedQuote.getAll, {
      mode: "cors",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    });
    expect(response).toEqual(expectedResponse);
  });

  test("getCypherB", async () => {
    // Arrange
    const request = GetCypherBDtoMock();
    const expectedResponse: GetCypherBResponseDto = GetCypherBResponseDtoMock();
    const mockedImplementation = () =>
      Promise.resolve({
        json() {
          return { data: expectedResponse };
        },
      });
    global.fetch = vi.fn(mockedImplementation) as Mock;
    // Act
    const response = await QuotesDataService.getCypherB(request);

    // Assert
    expect(fetch).toHaveBeenCalledTimes(1);
    expect(fetch).toHaveBeenCalledWith(StooqUrls.cypherB.get, {
      mode: "cors",
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(request),
    });
    expect(response).toEqual(expectedResponse);
  });
});
