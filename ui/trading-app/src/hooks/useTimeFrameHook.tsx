import { useEffect, useState, useCallback, useRef } from "react";
export interface useTimeFrameHookResponse {
  handleStartDateChange: (date: Date | null) => void;
  handleEndDateChange: (date: Date | null) => void;
  startDate: Date;
  endDate: Date;
  minDate: Date;
  maxDate: Date;
}

export const useTimeFrameHook = (
  inputDates: Date[]
): useTimeFrameHookResponse => {
  const [startDate, setStartDate] = useState(new Date(2023, 5, 24));
  const [endDate, setEndDate] = useState(new Date(2023, 5, 26));
  const [minDate, setMinDate] = useState(new Date());
  const [maxDate, setMaxDate] = useState(new Date());
  const hasRunEffect = useRef(false);

  const handleStartDateChange = useCallback((date: Date | null) => {
    if (date) {
      setStartDate(date);
    }
  }, []);

  const handleEndDateChange = useCallback((date: Date | null) => {
    if (date) {
      setEndDate(date);
    }
  }, []);

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
    handleStartDateChange,
    handleEndDateChange,
    startDate,
    endDate,
    minDate,
    maxDate,
  };
};
