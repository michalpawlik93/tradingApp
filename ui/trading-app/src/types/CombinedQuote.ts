import { Quote } from "./Quote";

export interface CombinedQuote extends Quote {
    rsi: number;
    sma: number;
  }
  