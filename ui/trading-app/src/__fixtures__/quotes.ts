import { ICypherBChartForm } from "../components/forms/CypherBChartForm";
import { ISrsiChartForm } from "../components/forms/SrsiChartForm";
import { AssetName } from "../consts/assetName";
import { AssetType } from "../consts/assetType";
import { Granularity } from "../consts/granularity";
import { TechnicalIndicators } from "../consts/technicalIndicators";
import { mfiSettingsDefault, sRsiSettingsDefault } from "../consts/technicalIndicatorsSettings";
import { TradeAction } from "../consts/tradeAction";
import { TradingStrategy } from "../consts/tradingStrategy";
import { GetCombinedQuotesRequestDto } from "../services/dtos/GetCombinedQuotesRequestDto";
import { GetCombinedQuotesResponseDto } from "../services/dtos/GetCombinedQuotesResponseDto";
import { GetCypherBDto } from "../services/dtos/GetCypherBDto";
import { GetCypherBResponseDto } from "../services/dtos/GetCypherBResponseDto";
import { CombinedQuote } from "../types/CombinedQuote";
import { CypherBQuote } from "../types/CypherBQuote";
import { MfiResult } from "../types/Mfi";
import { Quote } from "../types/Quote";
import { SrsiQuote } from "../types/SrsiQuote";
import { SrsiSignal } from "../types/SrsiSignal";
import { WaveTrendSignal } from "../types/WaveTrendSignal";

export const GetCombinedQuotesResponseDtoMock = (): GetCombinedQuotesResponseDto => ({
  quotes: [CombinedQuoteMock()],
  rsiSettings: {
    oversold: 70,
    overbought: 70,
  },
});

export const CombinedQuoteMock = (): CombinedQuote => ({
  ohlc: QuoteMock(),
  rsi: 58_022.103_273_740_51,
  srsiSignal: {
    stochK: 60_022.103_273_740_51,
    stochD: 61_022.103_273_740_51,
    tradeAction: 1,
  },
});

export const QuoteMock = (): Quote => ({
  date: "2024-05-02T00:20:00+00:00",
  open: 58_299.680_022_649_416,
  high: 58_299.834_458_412_43,
  low: 58_092.103_385_740_51,
  close: 58_109.135_881_766_06,
  volume: 45.509_066_589_999_99,
});

export const GetCypherBResponseDtoMock = (): GetCypherBResponseDto => ({
  quotes: [CypherBQuoteMock()],
});

export const CypherBQuoteMock = (override: Partial<CypherBQuote> = {}): CypherBQuote => ({
  ohlc: QuoteMock(),
  waveTrendSignal: WaveTrendMock(),
  mfiResult: MfiMock(),
  srsiSignal: SrsiMock(),
  ...override,
});

export const SrsiQuoteMock = (override: Partial<SrsiQuote> = {}): SrsiQuote => ({
  ohlc: QuoteMock(),
  srsiSignal: SrsiMock(),
  ...override,
});

export const SrsiMock = (): SrsiSignal => ({
  stochD: 12.1314,
  stochK: 13.1314,
  tradeAction: TradeAction.Buy,
});

export const WaveTrendMock = (): WaveTrendSignal => ({
  wt1: 12.1314,
  wt2: 13.1314,
  vwap: 11.1314,
  tradeAction: TradeAction.Buy,
});

export const MfiMock = (): MfiResult => ({
  mfi: 5.1314,
});

export const GetCombinedQuotesRequestDtoMock = (): GetCombinedQuotesRequestDto => ({
  indicators: [
    {
      technicalIndicator: TechnicalIndicators.Rsi,
      sideIndicators: [],
    },
    {
      technicalIndicator: TechnicalIndicators.Srsi,
      sideIndicators: [],
    },
  ],
  asset: {
    name: AssetName.ANC,
    type: AssetType.Cryptocurrency,
  },
  timeFrame: {
    granularity: Granularity.Daily,
    startDate: new Date(2023, 5, 24).toISOString(),
    endDate: new Date(2023, 6, 24).toISOString(),
  },
  settings: {
    srsiSettings: sRsiSettingsDefault(),
  },
});

export const GetCypherBDtoMock = (): GetCypherBDto => ({
  asset: {
    name: AssetName.ANC,
    type: AssetType.Cryptocurrency,
  },
  timeFrame: {
    granularity: Granularity.Daily,
    startDate: new Date(2023, 5, 24).toISOString(),
    endDate: new Date(2023, 6, 24).toISOString(),
  },
  settings: {
    srsiSettings: sRsiSettingsDefault(),
    waveTrendSettings: {
      channelLength: 8,
      averageLength: 8,
      movingAverageLength: 8,
      oversold: -60,
      overbought: 60,
      overboughtLevel2: 60,
      oversoldLevel2: -60,
    },
    mfiSettings: mfiSettingsDefault,
  },
});

export const SrsiFormMock = (): ISrsiChartForm => ({
  granularity: Granularity.Daily,
  startDate: new Date(2023, 5, 24),
  endDate: new Date(2023, 6, 24),
  assetName: AssetName.USDPLN,
  assetType: AssetType.Cryptocurrency,
  tradingStrategy: TradingStrategy.Scalping,
  enabled: true,
  channelLength: 14,
  stochKSmooth: 1,
  stochDSmooth: 2,
  overbought: 80,
  oversold: 20,
});

export const CipherBFormMock = (): ICypherBChartForm => ({
  granularity: Granularity.Daily,
  startDate: new Date(2023, 5, 24),
  endDate: new Date(2023, 6, 24),
  assetName: AssetName.USDPLN,
  assetType: AssetType.Cryptocurrency,
  tradingStrategy: TradingStrategy.Scalping,
});
