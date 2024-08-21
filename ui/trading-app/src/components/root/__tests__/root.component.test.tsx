import { render, screen } from "@testing-library/react";
import { vi } from "vitest";
import Root from "../root.component";

const REOOT_TEST_ID = "RootTestId";
vi.mock("../../navigation/TopBar", () => ({
  TopBar: () => <div data-testid={REOOT_TEST_ID} />,
}));
describe("Root component tests", () => {
  test("Root - renders", () => {
    // Arrange && Act
    render(<Root />);

    // Assert
    screen.getByTestId(REOOT_TEST_ID);
  });
});
