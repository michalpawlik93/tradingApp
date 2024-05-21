import { SimpleChartsView } from "../../views/SimpleChartsView";
import { AdvancedChartsView } from "../../views/AdvancedChartsView";
import { Layout } from "../navigation/Layout";
import { navigationRoutes } from "../../consts/navigationRoutes";
import { Typography } from "@mui/material";
import { RouteObject } from "react-router-dom";

export const routerConfig: RouteObject[] = [
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        path: navigationRoutes.SimpleCharts,
        element: <SimpleChartsView />,
        handle: {
          crumb: () => <Typography color="text.primary">Simple Charts</Typography>,
        },
      },
      {
        path: navigationRoutes.AdvancedCharts,
        element: <AdvancedChartsView />,
        handle: {
          crumb: () => <Typography color="text.primary">Advanced Charts</Typography>,
        },
      },
    ],
  },
];
