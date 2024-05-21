import { vi } from "vitest";
import "@testing-library/jest-dom/vitest";

global.fetch = vi.fn();
(global as any).cwr = vi.fn();
global.structuredClone = (obj: unknown) => JSON.parse(JSON.stringify(obj));

// set for apexcharts
const ResizeObserverMock = vi.fn(() => ({
  observe: vi.fn(),
  unobserve: vi.fn(),
  disconnect: vi.fn(),
}));

vi.stubGlobal("ResizeObserver", ResizeObserverMock);

vi.mock("./services/QuotesDataService");
