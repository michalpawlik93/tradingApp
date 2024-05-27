import { useTimeFrameHook, UseTimeFrameHookResponse } from "../hooks/useTimeFrameHook";
import { mockOf } from "./mockOf";

export const useTimeFrameHookMock = (
  override: Partial<UseTimeFrameHookResponse> | null = {},
): UseTimeFrameHookResponse => {
  if (override === null) {
    return datesMock;
  }
  return {
    ...datesMock,
    ...override,
  };
};

export const mockUseTimeFrameHook = (override: Partial<UseTimeFrameHookResponse> | null = {}) => {
  const mockedData = useTimeFrameHookMock(override);
  mockOf(useTimeFrameHook).mockReturnValue(mockedData);
  return mockedData;
};

const datesMock = {
  minDate: new Date("2024-12-17T03:24:00"),
  maxDate: new Date("2024-12-18T03:24:00"),
};
