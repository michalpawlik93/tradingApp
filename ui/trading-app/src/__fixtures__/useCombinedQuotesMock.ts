import { useCombinedQuotes, useCombinedQuotesResponse } from "../hooks/useCombinedQuotes";
import { mockOf } from "./mockOf";
import { CombinedQuoteMock } from "./quotes";

export const createUseCombinedQuotesMock = (
  override: Partial<useCombinedQuotesResponse> | null = {},
): useCombinedQuotesResponse => {
  if (override === null) {
    return {
      rsiQuotes: [],
      srsiQuotes: [],
    };
  }
  return {
    rsiQuotes: [CombinedQuoteMock()],
    srsiQuotes: [CombinedQuoteMock()],
    ...override,
  };
};

export const mockUseCombinedQuotes = (override: Partial<useCombinedQuotesResponse> | null = {}) => {
  const mockedData = createUseCombinedQuotesMock(override);
  mockOf(useCombinedQuotes).mockReturnValue(mockedData);
  return mockedData;
};
