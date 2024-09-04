import { renderHook, waitFor } from "@testing-library/react";
import { CipherBFormMock, SrsiFormMock } from "../../__fixtures__/quotes";
import {
  cypherBDefaultValues,
  srsiFormDefaultValues,
  useQuotesFormStore,
} from "../quotesFormStore";

describe("useQuotesFormStore", () => {
  test("default state - intial values are set correctly", () => {
    //Arrange
    //Act
    const { result } = renderHook(() => useQuotesFormStore());
    //Assert
    expect(result.current.srsiForm).toEqual(srsiFormDefaultValues);
    expect(result.current.cipherBForm).toEqual(cypherBDefaultValues);
  });

  test("setSrsisForm - values set correctly", async () => {
    //Arrange
    //Act
    const { result } = renderHook(() => useQuotesFormStore());
    result.current.setSrsisForm(SrsiFormMock());
    //Assert
    await waitFor(() => {
      expect(result.current.srsiForm).toEqual(SrsiFormMock());
    });
  });

  test("cipherBForm - values set correctly", async () => {
    //Arrange
    //Act
    const { result } = renderHook(() => useQuotesFormStore());
    result.current.setCipherBForm(CipherBFormMock());
    //Assert
    await waitFor(() => {
      expect(result.current.cipherBForm).toEqual(CipherBFormMock());
    });
  });
});
