import { useRouteError } from 'react-router-dom'
export const ErrorBoundary = (): JSX.Element => {
  const error = useRouteError() as ErrorResponse
  return (
    <div className="not-found-page-root">
      <h1 className="not-found-page-h1">{'Unexpected error occurred'}</h1>
      <p className="not-found-page-p-1">
        {
          'It looks like something went wrong, please contact your system administrator.'
        }
        {error.data}
      </p>
      <p className="not-found-page-p-2">
        {'Error Detials: '}
        {error.data}
      </p>
      <p className="not-found-page-p-3">
        {'Error Status: '}
        {error.statusText}
      </p>
    </div>
  )
}

interface ErrorResponse {
  data: string
  status: number
  statusText: string
}
