import { AppBar, Theme } from '@mui/material'
import { css } from '@emotion/react'

const sideBarCss = {
  sideBar: (theme: Theme) =>
    css({
      height: '100vh',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      width: '4rem',
      backgroundColor: theme.palette.common.backgroundLightestGrey,
      borderRightColor: theme.palette.common.black,
      borderRightStyle: 'solid',
      borderRightWidth: 1,
      paddingBottom: '2rem',
      position: 'fixed',
      zIndex: 1,
    }),
}

export const SideBar = () => {
  return <AppBar elevation={0} css={sideBarCss.sideBar}></AppBar>
}
