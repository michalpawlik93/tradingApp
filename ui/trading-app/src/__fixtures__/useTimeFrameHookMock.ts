import { useTimeFrameHook } from "../hooks/useTimeFrameHook";
import { MaxMinDate } from "../types/MaxMinDate";
import { mockOf } from "./mockOf";

export const UseTimeFrameHookMock = (override: Partial<MaxMinDate> | null = {}): MaxMinDate => {
  if (override === null) {
    return datesMock;
  }
  return {
    ...datesMock,
    ...override,
  };
};

export const mockUseTimeFrameHook = (override: Partial<MaxMinDate> | null = {}) => {
  const mockedData = UseTimeFrameHookMock(override);
  mockOf(useTimeFrameHook).mockReturnValue(mockedData);
  return mockedData;
};

const datesMock = {
  minDate: new Date("2024-12-17T03:24:00"),
  maxDate: new Date("2024-12-18T03:24:00"),
};
