import { AssetName } from "src/consts/assetName";
import { AssetType } from "src/consts/assetType";
import { Granularity } from "src/consts/granularity";
import { GetCombinedQuotesResponseDto } from "src/services/dtos/GetCombinedQuotesResponseDto";
import { GetCypherBDto } from "src/services/dtos/GetCypherBDto";
import { GetCypherBResponseDto } from "src/services/dtos/GetCypherBResponseDto";
import { GetQuotesRequestDto } from "src/services/dtos/GetQuotesRequestDto";
import { CombinedQuote } from "src/types/CombinedQuote";
import { CypherBQuote } from "src/types/CypherBQuote";
import { Quote } from "src/types/Quote";
import { WaveTrend } from "../types/WaveTrend";

export const GetCombinedQuotesResponseDtoMock = (): GetCombinedQuotesResponseDto => ({
  quotes: [CombinedQuoteMock()],
  rsiSettings: {
    oversold: 70,
    overbought: 70,
  },
});

export const CombinedQuoteMock = (): CombinedQuote => ({
  ohlc: QuoteMock(),
  rsi: 58022.10327374051,
  sma: 58072.10338574051,
});

export const QuoteMock = (): Quote => ({
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

export const CypherBQuoteMock = (): CypherBQuote => ({
  ohlc: QuoteMock(),
  waveTrend: WaveTrendMock(),
  mfi: 58073.10338574051,
  vwap: 58072.10338574051,
});

export const WaveTrendMock = (): WaveTrend => ({
  wt1: 12.1314,
  wt2: 13.1314,
  vwap: 11.1314,
  crossesOver: true,
  crossesUnder: true,
});

export const GetQuotesRequestDtoMock = (): GetQuotesRequestDto => ({
  technicalIndicators: [],
  granularity: Granularity.Daily,
  assetType: AssetType.Cryptocurrency,
  assetName: AssetName.BTC,
  startDate: "2023-01-01",
  endDate: "2023-12-31",
});

export const GetCypherBDtoMock = (): GetCypherBDto => ({
  asset: {
    name: AssetName.ANC,
    type: AssetType.Cryptocurrency,
  },
  timeFrame: {
    granularity: Granularity.Daily,
  },
  waveTrendSettings: {
    channelLength: 8,
    averageLength: 8,
    movingAverageLength: 8,
    oversold: 60,
    overbought: 60,
  },
  sRsiSettings: {
    enable: true,
    length: 8,
    stochKSmooth: 8,
    stochDSmooth: 8,
  },
});
