import { renderHook, waitFor } from "@testing-library/react";
import { TestingProvider } from "../../__fixtures__/TestingProvider";
import { useTimeFrameHook } from "../useTimeFrameHook";

vi.unmock("../useTimeFrameHook");
describe("useTimeFrameHook tests", () => {
  test("useTimeFrameHook - should fetch cypherb quotes", async () => {
    // Arrange
    const inputDates = [new Date(2023, 0, 1), new Date(2023, 1, 1), new Date(2023, 2, 1)];

    // Act
    const { result } = renderHook(useTimeFrameHook, {
      initialProps: inputDates,
      wrapper: TestingProvider,
    });

    // Assert
    await waitFor(() => {
      expect(result.current.maxDate).toEqual(new Date(2023, 2, 1));
      expect(result.current.minDate).toEqual(new Date(2023, 0, 1));
    });
  });
});
