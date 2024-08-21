import { vi } from "vitest";

import "@testing-library/jest-dom/vitest";

global.fetch = vi.fn();
(global as any).cwr = vi.fn();
global.structuredClone = (obj: unknown) => JSON.parse(JSON.stringify(obj));

vi.mock("echarts", () => ({
  getInstanceByDom: vi.fn(() => ({
    setOption: vi.fn(),
    clear: vi.fn(),
  })),
  init: vi.fn(),
}));

vi.mock("./services/QuotesDataService");

vi.mock("./hooks/useCombinedQuotes", () => ({
  useCombinedQuotes: vi.fn(),
}));
vi.mock("./hooks/useCypherBQuotes", () => ({
  useCypherBQuotes: vi.fn(),
}));
vi.mock("./hooks/useTimeFrameHook", () => ({
  useTimeFrameHook: vi.fn(),
}));
