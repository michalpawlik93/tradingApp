import {
  AppBar,
  IconButton,
  Slide,
  useScrollTrigger,
  Theme,
  Menu,
  MenuItem,
  Toolbar,
  useTheme,
} from "@mui/material";
import { css } from "@emotion/react";
import BarChartIcon from "@mui/icons-material/BarChart";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import { navigationRoutes } from "../../consts/navigationRoutes";
import { Breadcrumbs } from "./Breadcrumb";

const topBarCss = {
  toolBar: (theme: Theme) =>
    css({
      paddingLeft: "2px",
      paddingRight: "1.25rem",
      marginLeft: "auto",
      marginRight: "0px",
      color: theme.palette.common.brandBlue,
    }),
  appBar: (theme: Theme) =>
    css({
      backgroundColor: theme.palette.common.backgroundLightestGrey,
      boxShadow: "0 2px 4px 0 {theme.palette.common.brandBlue}",
      left: "4rem",
      width: "auto",
    }),
  iconStyle: (theme: Theme) =>
    css({
      color: theme.palette.common.chipColor,
      paddingRight: "1.25rem",
      padding: 0,
    }),
  breadCrumbsStyle: (theme: Theme) =>
    css({
      color: theme.palette.common.chipColor,
      paddingLeft: "1.25rem",
      padding: 0,
      marginLeft: "0px",
    }),
};

export const TopBar = () => {
  const theme: Theme = useTheme();
  const trigger = useScrollTrigger();
  const [anchor, setAnchor] = useState<null | HTMLElement>(null);
  const navigate = useNavigate();

  const handleClose = () => {
    setAnchor(null);
  };
  const navigateToSimpleCharts = () => {
    navigate(`/${navigationRoutes.SimpleCharts}`);
    setAnchor(null);
  };
  const navigateToAdvancedCharts = () => {
    navigate(`/${navigationRoutes.AdvancedCharts}`);
    setAnchor(null);
  };
  const handleMenu = (event: React.MouseEvent<HTMLElement>) => {
    setAnchor(event.currentTarget);
  };
  return (
    <Slide appear={false} direction="down" in={!trigger}>
      <AppBar elevation={0} css={topBarCss.appBar(theme)}>
        <Breadcrumbs css={topBarCss.breadCrumbsStyle(theme)} />
        <Toolbar css={topBarCss.toolBar(theme)}>
          <IconButton css={topBarCss.iconStyle(theme)}></IconButton>
          <IconButton
            size="large"
            aria-label="charts"
            aria-controls="menu-appbar"
            aria-haspopup="true"
            onClick={handleMenu}
            color="inherit"
          >
            <BarChartIcon />
          </IconButton>
          <Menu
            id="menu-appbar"
            anchorEl={anchor}
            anchorOrigin={{
              vertical: "top",
              horizontal: "right",
            }}
            keepMounted
            transformOrigin={{
              vertical: "top",
              horizontal: "right",
            }}
            open={Boolean(anchor)}
            onClose={handleClose}
          >
            <MenuItem onClick={navigateToSimpleCharts}>Simple Charts</MenuItem>
            <MenuItem onClick={navigateToAdvancedCharts}>Advanced Charts</MenuItem>
          </Menu>
        </Toolbar>
      </AppBar>
    </Slide>
  );
};
