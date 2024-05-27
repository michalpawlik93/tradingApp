import { useCombinedQuotes, useCombinedQuotesResponse } from "../hooks/useCombinedQuotes";
import { mockOf } from "./mockOf";
import { CombinedQuoteMock } from "./quotes";

export const createUseCombinedQuotesMock = (
  override: Partial<useCombinedQuotesResponse> | null = {},
): useCombinedQuotesResponse => {
  if (override === null) {
    return {
      combinedQuotes: [],
    };
  }
  return {
    combinedQuotes: [CombinedQuoteMock()],
    ...override,
  };
};

export const mockUseCombinedQuotes = (override: Partial<useCombinedQuotesResponse> | null = {}) => {
  const mockedData = createUseCombinedQuotesMock(override);
  mockOf(useCombinedQuotes).mockReturnValue(mockedData);
  return mockedData;
};
