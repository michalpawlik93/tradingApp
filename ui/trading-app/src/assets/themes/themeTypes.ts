import { CommonColors, TypeBackground } from "@mui/material/styles/createPalette";

interface GreyColor {
  50: string;
  100: string;
  400: string;
  500: string;
  700: string;
}

interface SimplePaletteColorOptions {
  main: string;
}

interface MainLightPaletteColorOptions {
  main: string;
  light: string;
}

interface PortalTypeText {
  primary: string;
}

export interface GeneralPaletteOptions {
  primary: SimplePaletteColorOptions;
  secondary: SimplePaletteColorOptions;
  error: MainLightPaletteColorOptions;
  warning: SimplePaletteColorOptions;
  info: MainLightPaletteColorOptions;
  success: SimplePaletteColorOptions;
  common: CommonColors;
  grey: GreyColor;
  text: PortalTypeText;
  background: TypeBackground;
}
