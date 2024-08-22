import { Mock } from "vitest";
import {
  GetCombinedQuotesRequestDtoMock,
  GetCombinedQuotesResponseDtoMock,
  GetCypherBDtoMock,
  GetCypherBResponseDtoMock,
} from "../../__fixtures__/quotes";
import { GetCombinedQuotesResponseDto } from "../dtos/GetCombinedQuotesResponseDto";
import { GetCypherBResponseDto } from "../dtos/GetCypherBResponseDto";
import { QuotesDataService } from "../QuotesDataService";
import { StooqUrls } from "../urls/stooqUrl";

vi.unmock("../QuotesDataService");
describe("QuotesDataService", () => {
  test("getCombinedQuotes", async () => {
    // Arrange
    const request = GetCombinedQuotesRequestDtoMock();
    const expectedResponse: GetCombinedQuotesResponseDto = GetCombinedQuotesResponseDtoMock();
    const mockedImplementation = () =>
      Promise.resolve({
        json() {
          return { data: expectedResponse };
        },
      });
    global.fetch = vi.fn(mockedImplementation) as Mock;

    // Act
    const response = await QuotesDataService.getCombinedQuotes(GetCombinedQuotesRequestDtoMock());

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
