import { useEffect, useState, useRef } from "react";

export interface UseTimeFrameHookResponse {
  minDate: Date;
  maxDate: Date;
}

export const useTimeFrameHook = (inputDates: Date[]): UseTimeFrameHookResponse => {
  const [minDate, setMinDate] = useState(new Date());
  const [maxDate, setMaxDate] = useState(new Date());
  const hasRunEffect = useRef(false);

  useEffect(() => {
    if (inputDates && inputDates.length > 0 && hasRunEffect.current == false) {
      const mapped = inputDates.map((date) => date.getTime());
      const max = new Date(Math.max(...mapped));
      const min = new Date(Math.min(...mapped));
      setMaxDate(max);
      setMinDate(min);
      hasRunEffect.current = true;
    }
  }, [inputDates, hasRunEffect]);

  return {
    minDate,
    maxDate,
  };
};
