import { GetCombinedQuotesResponseDto } from "src/services/dtos/GetCombinedQuotesResponseDto";
import { GetCypherBResponseDto } from "src/services/dtos/GetCypherBResponseDto";
import { CombinedQuote } from "src/types/CombinedQuote";
import { CypherBQuote } from "src/types/CypherBQuote";
import { Quote } from "src/types/Quote";

export const GetCombinedQuotesResponseDtoMock = (): GetCombinedQuotesResponseDto => ({
  quotes: [CombinedQuoteMock()],
  rsiSettings: {
    oversold: 70,
    overbought: 70,
  },
});

const CombinedQuoteMock = (): CombinedQuote => ({
  ohlc: QuoteMock(),
  rsi: 58022.10327374051,
  sma: 58072.10338574051,
});

const QuoteMock = (): Quote => ({
  date: "2024-05-02T00:20:00+00:00",
  open: 58299.680022649416,
  high: 58299.83445841243,
  low: 58092.10338574051,
  close: 58109.13588176606,
  volume: 45.50906658999999,
});

export const GetCypherBResponseDtoMock = (): GetCypherBResponseDto => ({
  quotes: [CypherBQuoteMock()],
});

const CypherBQuoteMock = (): CypherBQuote => ({
  ohlc: QuoteMock(),
  waveTrend: {
    value: 58073.10338579051,
    crossesOver: false,
    crossesUnder: false,
  },
  mfi: 58073.10338574051,
  vwap: 58072.10338574051,
});
