import {
    AppBar,
    IconButton,
    Slide,
    useScrollTrigger,
    Theme,
    Toolbar,
  } from '@mui/material'
  import { css } from '@emotion/react'
  
  const topBarCss = {
    topBar: (theme: Theme) =>
      css({
        paddingLeft: '2px',
        paddingRight: '1.25rem',
        marginLeft: 'auto',
        marginRight: '0px',
        color: theme.palette.common.brandBlue,
      }),
    appBar: (theme: Theme) =>
      css({
        backgroundColor: theme.palette.common.backgroundLightestGrey,
        boxShadow: '0 2px 4px 0 {theme.palette.common.brandBlue}',
        left: '4rem',
        width: 'auto',
      }),
    iconStyle: (theme: Theme) =>
      css({
        color: theme.palette.common.chipColor,
        paddingRight: '1.25rem',
        padding: 0,
      }),
  }
  
  export const TopBar = () => {
    const trigger = useScrollTrigger()
    return (
      <Slide appear={false} direction="down" in={!trigger}>
        <AppBar  elevation={0} css={topBarCss.appBar}>
          <Toolbar css={topBarCss.topBar}>
            <IconButton css={topBarCss.iconStyle}>
            </IconButton>
          </Toolbar>
        </AppBar>
      </Slide>
    )
  }
  