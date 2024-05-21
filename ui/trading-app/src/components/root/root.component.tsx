import React from "react";
import { StyledEngineProvider, ThemeProvider } from "@mui/material";
import { createTheme } from "../../assets/themes/createTheme";
import { RouterProvider } from "react-router-dom";
import { RouterFallbackSpinner } from "../presentational/RouterFallbackSpinner";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDateFns } from "@mui/x-date-pickers/AdapterDateFns";
import "../../assets/root.css";
import { ErrorBoundary } from "react-error-boundary";
import ErrorFallback from "./ErrorFallback";
import { router } from "./router";

const Root: React.FC = () => {
  return (
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
};

export default Root;
