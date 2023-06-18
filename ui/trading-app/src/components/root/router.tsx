import { createBrowserRouter } from "react-router-dom";
import { SimpleChartsView } from "../../views/SimpleChartsView";
import { AdvancedChartsView } from "../../views/AdvancedChartsView";
import { ErrorBoundary } from "./ErrorRouterBoundary";
import { Layout } from "../navigation/Layout";
import { navigationRoutes } from "../../consts/navigationRoutes";
import { Typography } from "@mui/material";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    errorElement: <ErrorBoundary />,
    children: [
      {
        path: navigationRoutes.SimpleCharts,
        element: <SimpleChartsView />,
        handle: {
          crumb: () => (
            <Typography color="text.primary">Simple Charts</Typography>
          ),
        },
      },
      {
        path: navigationRoutes.AdvancedCharts,
        element: <AdvancedChartsView />,
        handle: {
          crumb: () => (
            <Typography color="text.primary">Advanced Charts</Typography>
          ),
        },
      },
    ],
  },
]);
