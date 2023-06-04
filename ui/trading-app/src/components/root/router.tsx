import { createBrowserRouter } from 'react-router-dom'
import { ChartHomeView } from '../../views/ChartHomeView'
import { ErrorBoundary } from './ErrorRouterBoundary'
import { Layout } from '../navigation/Layout'

export const router = createBrowserRouter([
  {
    path: '/',
    element: <Layout />,
    errorElement: <ErrorBoundary />,
    children: [
      {
        path: 'charts',
        element: <ChartHomeView />,
      },
    ],
  },
])
