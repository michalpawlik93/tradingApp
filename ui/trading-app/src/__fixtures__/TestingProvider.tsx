import { ReactNode } from "react";
import { StyledEngineProvider, ThemeProvider } from "@mui/material";
import { RouteObject, RouterProvider, createMemoryRouter } from "react-router-dom";
import { createTheme } from "../assets/themes/createTheme";
import { LocalizationProvider } from "@mui/x-date-pickers";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";

interface TestingProviderProps {
  children?: ReactNode;
  overrideRouter?: RouterProps;
}

export interface RouterProps {
  routes: RouteObject[];
  initialEntries: string[];
}

export const TestingProvider = ({ children, overrideRouter }: TestingProviderProps) => {
  const theme = createTheme();
  const memoryRouter = overrideRouter
    ? createMemoryRouter(overrideRouter.routes, {
        initialEntries: overrideRouter.initialEntries,
      })
    : createMemoryRouter(routerConfig(children), {
        initialEntries: ["/testpath"],
      });

  return (
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <StyledEngineProvider injectFirst>
        <ThemeProvider theme={theme}>
          <RouterProvider router={memoryRouter} />
        </ThemeProvider>
      </StyledEngineProvider>
    </LocalizationProvider>
  );
};

export const routerConfig = (children: ReactNode) => [
  {
    path: "/",
    children: [
      {
        path: "testpath",
        element: <>{children}</>,
        handle: {
          crumb: () => <div>Test Router</div>,
        },
      },
    ],
  },
];
