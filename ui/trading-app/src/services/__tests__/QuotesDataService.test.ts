import {
  GetCombinedQuotesResponseDtoMock,
  GetCypherBResponseDtoMock,
} from "src/__fixtures__/quotes";
import { QuotesDataService } from "../QuotesDataService";
import { GetCombinedQuotesResponseDto } from "../dtos/GetCombinedQuotesResponseDto";
import { GetCypherBDto } from "../dtos/GetCypherBDto";
import { GetCypherBResponseDto } from "../dtos/GetCypherBResponseDto";
import { GetQuotesRequestDto } from "../dtos/GetQuotesRequestDto";
import { StooqUrls } from "../urls/stooqUrl";
import { Mock } from "vitest";

vi.unmock("../QuotesDataService");
describe("QuotesDataService", () => {
  test("getCombinedQuotes", async () => {
    // Arrange
    const request: GetQuotesRequestDto = {
      technicalIndicators: [],
      granularity: "daily",
      assetType: "stock",
      assetName: "AAPL",
      startDate: "2023-01-01",
      endDate: "2023-12-31",
    };

    const expectedResponse: GetCombinedQuotesResponseDto = GetCombinedQuotesResponseDtoMock();
    const mockedImplementation = () =>
      Promise.resolve({
        json() {
          return { data: expectedResponse };
        },
      });
    global.fetch = vi.fn(mockedImplementation) as Mock;

    // Act
    const response = await QuotesDataService.getCombinedQuotes(request);

    // Assert
    expect(fetch).toHaveBeenCalledTimes(1);
    expect(fetch).toHaveBeenCalledWith(
      `${StooqUrls.combinedQuote.getAll}?granularity=daily&assetType=stock&assetName=AAPL&startDate=2023-01-01&endDate=2023-12-31`,
      {
        mode: "cors",
        method: "GET",
        headers: {
          "Content-Type": "application/json",
        },
        body: undefined,
      },
    );
    expect(response).toEqual(expectedResponse);
  });

  test("getCypherB", async () => {
    // Arrange
    const request: GetCypherBDto = {
      asset: {
        name: "",
        type: "",
      },
      timeFrame: {
        granularity: "",
      },
      waveTrendSettings: {
        channelLength: 0,
        averageLength: 0,
        movingAverageLength: 0,
        oversold: 0,
        overbought: 0,
      },
      sRsiSettings: {
        enable: false,
        length: 0,
        stochKSmooth: 0,
        stochDSmooth: 0,
      },
    };

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
    expect(fetch).toHaveBeenCalledWith(`${StooqUrls.cypherB.get}`, {
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
