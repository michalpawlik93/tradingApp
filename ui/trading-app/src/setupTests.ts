import { vi } from "vitest";
import "@testing-library/jest-dom/vitest";

globalThis.jest = vi;

global.fetch = vi.fn();
// eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
(global as any).cwr = vi.fn();
// eslint-disable-next-line @typescript-eslint/no-unsafe-return
global.structuredClone = (obj: unknown) => JSON.parse(JSON.stringify(obj));
