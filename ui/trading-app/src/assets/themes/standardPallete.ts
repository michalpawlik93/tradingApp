import { calculateContrastColor } from "./colorUtilities";
import { GeneralPaletteOptions } from "./themeTypes";

declare module "@mui/material/styles/createPalette" {
  interface CommonColors {
    black: string;
    white: string;
    headerColor: string;
    headerIcon: string;
    dropzoneBackground: string;
    inactiveIcon: string;
    activeIcon: string;
    linkColor: string;
    buttonFocusBorder: string;
    chipColor: string;
    inputBackgorund: string;
    hubProgressBarHeadingColor: string;
    brandBlue: string;
    backgroundLighterGrey: string;
    backgroundLightestGrey: string;
  }
}

const uxStandardColors = {
  brandBlue: "#3b75b3",
  backgroundWhite: "#fff",
  backgroundBlack: "#000524",
  backgroundDarkGrey: "#4e647b",
  backgroundGrey: "#96a8ba",
  backgroundLightGrey: "#c0cedc",
  backgroundLighterGrey: "#e6ebf1",
  backgroundLightestGrey: "#f5f7f9",
  // error, info, success, warning colors
  highlightsRed: "#e57474",
  highlightsGreen: "#73ae61",
  highlightsAmber: "#ed992d",
  highlightsBlue: "#5a9adf",
  highlightsCyan: "#a0e4ff", // going to be used for focus state of the buttons (borders)
  // other
  inputSelectionStatesFieldFocusBlue: "#eff8ff",
  inputSelectionStatesFieldErrorRed: "#fff6f6",
  inputSelectionStatesFieldDefaultGrey: "#fafbfc",
  backgroundChipGrey: "#d3d9de",
  highlightsLinkBlue: "#0071eb",
};

export const createStandardPalette = (): GeneralPaletteOptions => {
  const primaryColor = uxStandardColors.brandBlue;
  const headerColor = uxStandardColors.backgroundWhite;
  const errorColor = uxStandardColors.highlightsRed;
  const successColor = uxStandardColors.highlightsGreen;
  const warningColor = uxStandardColors.highlightsAmber;
  const infoColor = uxStandardColors.highlightsBlue;
  const headerIcon = calculateContrastColor(
    headerColor,
    uxStandardColors.backgroundWhite,
    uxStandardColors.backgroundGrey,
  );

  return {
    primary: {
      main: primaryColor,
    },
    secondary: {
      main: uxStandardColors.backgroundDarkGrey,
    },
    background: {
      default: uxStandardColors.backgroundWhite,
      paper: uxStandardColors.backgroundWhite,
    },
    grey: {
      "700": uxStandardColors.backgroundDarkGrey,
      "500": uxStandardColors.backgroundGrey,
      "400": uxStandardColors.backgroundLightGrey,
      "100": uxStandardColors.backgroundLighterGrey,
      "50": uxStandardColors.backgroundLightestGrey,
    },
    text: {
      primary: uxStandardColors.backgroundBlack,
    },
    success: {
      main: successColor,
    },
    info: {
      main: infoColor,
      light: uxStandardColors.inputSelectionStatesFieldFocusBlue,
    },
    warning: {
      main: warningColor,
    },
    error: {
      main: errorColor,
      light: uxStandardColors.inputSelectionStatesFieldErrorRed,
    },
    common: {
      black: uxStandardColors.backgroundBlack,
      white: uxStandardColors.backgroundWhite,
      headerColor,
      headerIcon,
      dropzoneBackground: uxStandardColors.backgroundGrey,
      inactiveIcon: uxStandardColors.backgroundLightGrey,
      activeIcon: uxStandardColors.backgroundDarkGrey,
      linkColor: uxStandardColors.highlightsLinkBlue,
      buttonFocusBorder: uxStandardColors.highlightsCyan,
      chipColor: uxStandardColors.backgroundChipGrey,
      inputBackgorund: uxStandardColors.inputSelectionStatesFieldDefaultGrey,
      hubProgressBarHeadingColor: uxStandardColors.backgroundDarkGrey,
      brandBlue: uxStandardColors.brandBlue,
      backgroundLightestGrey: uxStandardColors.backgroundLightestGrey,
      backgroundLighterGrey: uxStandardColors.backgroundLightGrey,
    },
  };
};
