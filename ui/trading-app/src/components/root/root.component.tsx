import React from "react";
import { StyledEngineProvider, ThemeProvider } from "@mui/material";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { ErrorBoundary } from "react-error-boundary";
import { RouterProvider } from "react-router-dom";
import { createTheme } from "../../assets/themes/createTheme";
import { RouterFallbackSpinner } from "../presentational/RouterFallbackSpinner";
import ErrorFallback from "./ErrorFallback";
import { router } from "./router";

import "../../assets/root.css";

const Root = () => (
  <ErrorBoundary FallbackComponent={ErrorFallback}>
    <LocalizationProvider dateAdapter={AdapterDateFns}>
      <StyledEngineProvider injectFirst>
        <ThemeProvider theme={createTheme()}>
          <RouterProvider router={router} fallbackElement={<RouterFallbackSpinner />} />
        </ThemeProvider>
      </StyledEngineProvider>
    </LocalizationProvider>
  </ErrorBoundary>
);

export default Root;
