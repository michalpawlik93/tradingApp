import { create } from "zustand";
import { assetFormDefaultValues } from "../components/forms/AssetFormElements";
import { ICypherBChartForm } from "../components/forms/CypherBChartForm";
import { ISrsiChartForm } from "../components/forms/SrsiChartForm";
import { srsiSettingsFormDefaultValues } from "../components/forms/SrsiSettingsFormElements";
import { timeFrameFormDefaultValues } from "../components/forms/TimeFrameFormElements";
import { tradingStreategyDefaultValues } from "../components/forms/TradingStreategyFormElement";

interface QuotesFormState {
  srsiForm: ISrsiChartForm;
  cipherBForm: ICypherBChartForm;
  setSrsisForm: (request: ISrsiChartForm) => void;
  setCipherBForm: (request: ICypherBChartForm) => void;
}

export const srsiFormDefaultValues: ISrsiChartForm = {
  ...timeFrameFormDefaultValues(),
  ...assetFormDefaultValues(),
  ...tradingStreategyDefaultValues(),
  ...srsiSettingsFormDefaultValues(),
};

export const cypherBDefaultValues: ICypherBChartForm = {
  ...timeFrameFormDefaultValues(),
  ...assetFormDefaultValues(),
  ...tradingStreategyDefaultValues(),
};

export const useQuotesFormStore = create<QuotesFormState>((set) => ({
  srsiForm: srsiFormDefaultValues,
  cipherBForm: cypherBDefaultValues,
  setSrsisForm: (request: ISrsiChartForm) => {
    set({
      srsiForm: request,
    });
  },
  setCipherBForm: (request: ICypherBChartForm) => {
    set({
      cipherBForm: request,
    });
  },
}));
