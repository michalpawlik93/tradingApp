import { IQuotesDataService } from "src/services/IQuotesDataService";
import { GetCombinedQuotesResponseDtoMock, GetCypherBResponseDtoMock } from "./quotes";

export const createQuotesDataServiceMock = (): IQuotesDataService => ({
  getCombinedQuotes: vi.fn().mockResolvedValue(GetCombinedQuotesResponseDtoMock()),
  getCypherB: vi.fn().mockResolvedValue(GetCypherBResponseDtoMock()),
});
