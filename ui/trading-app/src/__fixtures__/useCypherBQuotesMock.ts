import { useCypherBQuotes, useCypherBQuotesResponse } from "../hooks/useCypherBQuotes";
import { mockOf } from "./mockOf";
import { CypherBQuoteMock } from "./quotes";

export const useCypherBQuotesMock = (
  override: Partial<useCypherBQuotesResponse> | null = {},
): useCypherBQuotesResponse => {
  if (override === null) {
    return {
      cypherBQuotes: [],
    };
  }
  return {
    cypherBQuotes: [CypherBQuoteMock()],
    ...override,
  };
};

export const mockUseCypherBQuotes = (override: Partial<useCypherBQuotesResponse> | null = {}) => {
  const mockedData = useCypherBQuotesMock(override);
  mockOf(useCypherBQuotes).mockReturnValue(mockedData);
  return mockedData;
};
