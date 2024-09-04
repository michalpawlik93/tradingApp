import { useEffect, useRef, useState } from "react";
import { MaxMinDate } from "../types/MaxMinDate";

export const useTimeFrameHook = (inputDates: Date[]): MaxMinDate => {
  const [maxMinDate, setMaxMinDate] = useState<MaxMinDate>({
    minDate: undefined,
    maxDate: undefined,
  });
  const hasRunEffect = useRef(false);

  useEffect(() => {
    if (inputDates && inputDates.length > 0 && hasRunEffect.current == false) {
      const mapped = inputDates.map((date) => date.getTime());
      const max = new Date(Math.max(...mapped));
      const min = new Date(Math.min(...mapped));
      setMaxMinDate({ minDate: min, maxDate: max });
      hasRunEffect.current = true;
    }
  }, [inputDates, hasRunEffect]);

  return maxMinDate;
};
