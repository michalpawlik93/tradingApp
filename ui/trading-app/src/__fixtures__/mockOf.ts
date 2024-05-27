import { Mock } from "vitest";

export const mockOf = <P extends unknown[], R>(fn: (...args: P) => R) => fn as Mock<P, R>;
