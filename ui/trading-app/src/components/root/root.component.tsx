import React from 'react'
import { StyledEngineProvider, ThemeProvider } from '@mui/material'
import { createTheme } from '../../assets/themes/createTheme'
import { RouterProvider } from 'react-router-dom'
import { RouterFallbackSpinner } from '../presentational/RouterFallbackSpinner'
import { router } from './router'
import '../../assets/root.css'

const Root: React.FC = () => {
  const theme = createTheme()
  return (
    <StyledEngineProvider injectFirst>
      <ThemeProvider theme={theme}>
        <RouterProvider
          router={router}
          fallbackElement={<RouterFallbackSpinner/>}
        />
      </ThemeProvider>
    </StyledEngineProvider>
  )
}

export default Root
