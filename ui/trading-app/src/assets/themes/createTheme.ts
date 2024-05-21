import {
  Theme as MuiTheme,
  createTheme as createMuiTheme,
  buttonClasses,
  inputLabelClasses,
  linkClasses,
  stepConnectorClasses,
  stepLabelClasses,
  inputBaseClasses,
  chipClasses,
  formHelperTextClasses,
  autocompleteClasses,
  tabsClasses,
  accordionSummaryClasses,
  listItemButtonClasses,
  iconButtonClasses,
  darken,
  alertClasses,
  menuItemClasses,
  switchClasses,
} from "@mui/material";
import { createStandardPalette } from "./standardPallete";
import "@emotion/react";

declare module "@emotion/react" {
  // eslint-disable-next-line @typescript-eslint/no-empty-interface
  export interface Theme extends MuiTheme {}
}

export const createTheme = (): MuiTheme => {
  const palette = createStandardPalette();

  return createMuiTheme({
    palette: palette,
    typography: {
      fontFamily: "OpenSans, sans-serif",
      h1: {
        fontSize: "1.5rem",
        fontWeight: "bold",
        lineHeight: 1.33,
        marginBottom: "30px",
      },
      h2: {
        fontSize: "1.25rem",
        fontWeight: "bold",
        lineHeight: 1.6,
      },
      h3: {
        fontSize: "1rem",
        fontWeight: "bold",
        lineHeight: 1.5,
      },
      h4: {
        fontSize: "0.875rem",
        fontWeight: "bold",
        lineHeight: 1.71,
      },
      h5: {
        fontSize: "0.875rem",
        fontWeight: 700,
        lineHeight: 1.71,
      },
      h6: {
        fontSize: "0.875rem",
        fontWeight: 700,
        lineHeight: 1.71,
      },
      subtitle1: {
        fontSize: "0.875rem",
        fontWeight: 600,
        lineHeight: 1.43,
      },
      subtitle2: {
        fontSize: "0.875rem",
        fontWeight: 300,
        lineHeight: 1.43,
      },
      body1: {
        fontSize: "1rem",
        fontWeight: 400,
        lineHeight: 1.43,
      },
      body2: {
        fontSize: "0.875rem",
        fontWeight: 400,
        lineHeight: 1.71,
      },
      caption: {
        fontSize: "0.75rem",
        fontWeight: 400,
        lineHeight: 1.33,
      },
    },
    components: {
      MuiAppBar: {
        styleOverrides: {
          root: {
            top: "-65px",
            padding: "0 1.5rem",
            justifyContent: "space-between",
            minHeight: "3.5rem",
            flexDirection: "row",
            alignItems: "center",
            border: "none",
            borderBottomStyle: "solid",
            borderBottomWidth: 1,
            borderBottomColor: palette.grey[400],
            borderRadius: 0,
          },
          colorPrimary: {
            position: "sticky",
            backgroundColor: palette.common.headerColor,
            boxShadow: "none",
          },
        },
      },
      MuiLinearProgress: {
        styleOverrides: {
          root: {
            height: "0.5rem",
            backgroundColor: palette.grey[400],
          },
          barColorPrimary: {
            backgroundColor: palette.info.main,
          },
        },
      },
      MuiLink: {
        defaultProps: {
          underline: "hover",
        },
        styleOverrides: {
          root: {
            color: palette.common.linkColor,
            [`&.${linkClasses.underlineHover}:focus`]: {
              backgroundColor: palette.common.linkColor,
              color: palette.common.white,
            },
          },
        },
      },
      MuiFormControl: {
        styleOverrides: {
          marginDense: {
            marginTop: "0.75rem",
            marginBottom: "0.5rem",
          },
        },
      },
      MuiInputLabel: {
        styleOverrides: {
          root: {
            color: palette.secondary.main,
            fontSize: "0.875rem",
            fontWeight: 600,
            textAlign: "left",
            lineHeight: 1.71,
            wordBreak: "break-word",
            whiteSpace: "normal",

            [`& .${inputLabelClasses.asterisk}`]: {
              color: palette.error.main,
            },
            [`&.${inputLabelClasses.focused}:not(.${inputLabelClasses.error})`]: {
              color: palette.secondary.main,
            },
            [`&.${inputLabelClasses.shrink}`]: {
              transform: "scale(1)",
            },
            [`&.${inputLabelClasses.formControl}`]: {
              position: "relative",
            },
            [`&.${inputLabelClasses.disabled}`]: {
              color: palette.secondary.main,
            },
          },
        },
      },
      MuiInputBase: {
        styleOverrides: {
          root: {
            minHeight: "2.5rem",
            color: palette.text.primary,
            backgroundColor: palette.common.inputBackgorund,
            borderWidth: 1,
            borderStyle: "solid",
            borderColor: palette.grey[500],
            borderRadius: 4,
            padding: "0.5rem",
            fontSize: "0.875rem",

            [`&.${inputBaseClasses.focused}`]: {
              backgroundColor: palette.info.light,
              boxShadow: `0 0 3px 0 ${palette.info.main}`,
              borderWidth: 1,
              borderColor: palette.info.main,
            },
            [`&.${inputBaseClasses.error}`]: {
              borderColor: palette.error.main,
              backgroundColor: palette.error.light,
              borderWidth: 2,
            },
            [`&.${inputBaseClasses.disabled}`]: {
              color: palette.text.primary,
              opacity: 1,
            },
            [`&.${inputBaseClasses.adornedStart}`]: {
              "& > svg": {
                color: palette.grey[700],
                marginRight: "0.5rem",
              },
            },
          },
          input: {
            padding: 0,
            "&::placeholder": { opacity: 1 },
          },
        },
      },
      MuiTablePagination: {
        styleOverrides: {
          selectRoot: {
            padding: 0,
          },
          menuItem: {
            textAlign: "center",
          },
        },
      },
      MuiInput: {
        defaultProps: { disableUnderline: true },
        styleOverrides: {
          root: {
            [`label + &.${inputBaseClasses.root}`]: {
              marginTop: 0,
            },
          },
          input: {
            "&:disabled": {
              WebkitTextFillColor: palette.text.primary,
            },
          },
        },
      },
      MuiFormHelperText: {
        styleOverrides: {
          root: {
            [`&.${formHelperTextClasses.error}`]: {
              color: palette.error.main,
              fontWeight: 600,
            },
          },
        },
      },
      MuiAutocomplete: {
        styleOverrides: {
          tag: {
            marginBottom: 1,
            marginTop: 0,
          },
          inputRoot: {
            [`&.${inputBaseClasses.root}`]: {
              rowGap: 4,
              paddingBottom: "0.25rem",
              paddingTop: "0.25rem",
              minHeight: "2.5rem",
              input: {
                padding: 0,
                "&::placeholder": { opacity: 1 },
              },
            },
          },
          endAdornment: {
            top: -2,
            [`& .${iconButtonClasses.root}`]: {
              margin: "0.5rem",
              width: "1.5rem",
              height: "1.5rem",
              padding: 0,
              color: palette.grey[700],
            },
          },
          paper: {
            borderWidth: 1,
            boxShadow: `0 1px 2px 0 ${palette.grey[400]}`,
            borderStyle: "solid",
            borderColor: palette.grey[400],
            borderTopWidth: 0,
            color: palette.grey[700],
            margin: "0 -0.125rem",
          },
          noOptions: {
            color: palette.grey[700],
          },
          popupIndicator: {
            marginRight: "1.5rem",
          },
          clearIndicator: {
            color: palette.grey[700],
          },
          listbox: {
            [`& .${autocompleteClasses.option}`]: {
              [`&[aria-selected="true"]`]: {
                backgroundColor: palette.info.light,
              },
              [`&.${autocompleteClasses.focused}`]: {
                backgroundColor: palette.grey[50],
              },
              "&:active": {
                backgroundColor: palette.info.light,
              },
            },
          },
        },
      },
      MuiMenu: {
        styleOverrides: {
          list: {
            borderWidth: 1,
            boxShadow: `0 1px 2px 0 ${palette.grey[400]}`,
            borderStyle: "solid",
            borderColor: palette.grey[400],
            color: palette.grey[700],

            [`li.${menuItemClasses.selected}`]: {
              backgroundColor: palette.info.light,
            },
            [`li.${menuItemClasses.focusVisible}`]: {
              backgroundColor: palette.grey[50],
            },
            "li:active, li:hover": {
              backgroundColor: palette.grey[50],
            },
          },
        },
      },
      MuiChip: {
        styleOverrides: {
          root: {
            color: palette.text.primary,
            borderRadius: 20,
            height: 27,
            [`&:not(.${chipClasses.disabled})`]: {
              backgroundColor: palette.common.chipColor,
            },
          },
          deleteIcon: {
            color: palette.grey[700],
            opacity: 0.8,
          },
          label: {
            fontWeight: 700,
            color: palette.grey[700],
          },
        },
      },
      MuiPaper: {
        styleOverrides: {
          root: {
            boxShadow: `0 1px 2px 0 ${palette.grey[400]}`,
            borderStyle: "solid",
            borderWidth: 1,
            borderRadius: 4,
            borderColor: palette.grey[400],
            backgroundColor: palette.common.white,
          },
        },
      },
      MuiButton: {
        styleOverrides: {
          root: {
            fontWeight: 600,
            fontSize: "1rem",

            [`&.${buttonClasses.contained}`]: {
              boxShadow: "none",
              borderStyle: "solid",
              borderWidth: 1,
              borderColor: palette.primary.main,

              "&:hover": {
                backgroundColor: darken(palette.primary.main, 0.25),
                boxShadow: `0 1px 2px 0 ${palette.grey[500]}`,
              },
              "&:focus": {
                boxShadow: `0 0 4px 1px ${palette.info.main}`,
                borderWidth: 1,
                borderColor: "transparent",
                backgroundColor: darken(palette.primary.main, 0.15),
              },
              [`&.${buttonClasses.disabled}`]: {
                color: palette.common.white,
                backgroundColor: palette.grey[400],
                borderColor: "transparent",
              },
            },
            [`&.${buttonClasses.outlined}`]: {
              borderWidth: 2,
              borderStyle: "solid",
              borderColor: palette.primary.main,
              color: palette.secondary.main,
              "&:focus": {
                boxShadow: `0 0 4px 1px ${palette.info.main}`,
                backgroundColor: palette.grey[50],
              },
              [`&.${buttonClasses.disabled}`]: {
                color: palette.grey[500],
                borderColor: palette.grey[500],
              },
            },
          },
        },
      },
      MuiStepLabel: {
        styleOverrides: {
          label: {
            fontWeight: 500,
            fontSize: "0.75rem",
            [`&.${stepLabelClasses.active}, &.${stepLabelClasses.completed}`]: {
              fontWeight: 700,
            },
          },
        },
      },
      MuiStepConnector: {
        styleOverrides: {
          root: {
            [`& .${stepConnectorClasses.line}`]: {
              borderColor: palette.grey[400],
              borderRadius: 4,
              borderWidth: 4,
            },
          },
        },
      },
      MuiStepButton: {
        styleOverrides: {
          root: {
            padding: 16,
          },
        },
      },
      MuiTextField: {
        defaultProps: {
          variant: "standard",
        },
        styleOverrides: {
          root: {
            [`div.${inputBaseClasses.formControl}.${inputLabelClasses.disabled}`]: {
              opacity: 1,
              borderRadius: "4px",
              borderWidth: 1,
              borderColor: palette.grey[500],
              WebkitTextFillColor: palette.text.primary,
              borderStyle: "dashed",
              backgroundColor: palette.grey[50],
            },
            [`label.${inputLabelClasses.disabled}`]: {
              opacity: 1,
            },
          },
        },
      },
      MuiInputAdornment: {
        styleOverrides: {
          positionEnd: {
            [`& .${iconButtonClasses.root}`]: {
              color: palette.grey[700],
            },
          },
        },
      },
      MuiTabs: {
        styleOverrides: {
          root: {
            [`& .${tabsClasses.indicator}`]: {
              height: 8,
              borderRadius: 4,
            },
            [`& button`]: {
              padding: "1rem 1.5rem",
              color: palette.grey[700],
              marginRight: "0.75rem",
              [`&[aria-selected="true"]`]: {
                fontWeight: "bold",
                color: palette.grey[700],
              },
            },
            [`& button:after`]: {
              content: '""',
              height: 8,
              borderRadius: 4,
              backgroundColor: palette.grey[50],
              position: "absolute",
              width: "90%",
              bottom: 0,
            },
          },
          indicator: {
            backgroundColor: palette.primary.main,
          },
        },
      },
      MuiListItemButton: {
        styleOverrides: {
          root: {
            padding: "0.5rem 1rem",
            borderRadius: 4,
            [`&.${listItemButtonClasses.selected}`]: {
              backgroundColor: palette.grey[50],
              borderRightWidth: "0.5rem",
              borderRightStyle: "solid",
              borderRightColor: palette.primary.main,
            },
            [`&[role="button"]:hover`]: {
              backgroundColor: darken(palette.grey[50], 0.05),
            },
          },
        },
      },
      MuiAccordion: {
        styleOverrides: {
          root: {
            backgroundColor: palette.grey[50],
          },
        },
      },
      MuiAccordionSummary: {
        styleOverrides: {
          content: {
            margin: "0.75rem 0",
            [`&.${accordionSummaryClasses.expanded}`]: {
              margin: "0.75rem 0",
            },
          },
        },
      },
      MuiDialogActions: {
        styleOverrides: {
          root: {
            padding: "1rem 2rem 2rem 2rem",
          },
        },
      },
      MuiDialogTitle: {
        styleOverrides: {
          root: {
            padding: 0,
            margin: 0,
          },
        },
      },
      MuiDialogContent: {
        styleOverrides: {
          root: {
            minWidth: "500px",
            minHeight: "50px",
            padding: "0rem 2rem 0rem 2rem",
          },
        },
      },
      MuiDialogContentText: {
        styleOverrides: {
          root: {
            color: palette.grey[700],
          },
        },
      },
      MuiAlert: {
        styleOverrides: {
          root: {
            padding: "0.5rem 1rem",
            borderWidth: 2,
            borderStyle: "solid",
            borderColor: "none",
            boxShadow: "none",
            marginBottom: "0.5rem",
            [`& .${alertClasses.message}`]: {
              display: "flex",
              padding: 0,
            },
          },
          standardInfo: {
            borderColor: "none",
            backgroundColor: palette.info.light,
          },
          standardError: {
            backgroundColor: palette.error.light,
            borderColor: "none",
          },
        },
      },
      MuiSwitch: {
        styleOverrides: {
          root: {
            padding: 0,
            margin: 0,
            [`& .${switchClasses.switchBase}`]: {
              padding: 2,
            },
            [`&.${switchClasses.sizeSmall}`]: {
              [`& .${switchClasses.thumb}`]: {
                width: 12,
                height: 12,
              },
              width: "2rem",
              height: "1rem",
            },
            [`&.${switchClasses.sizeMedium}`]: {
              width: "2.75rem",
              height: "1.5rem",
              [`& .${switchClasses.thumb}`]: {
                width: 20,
                height: 20,
              },
            },
          },
          switchBase: {
            color: palette.background.default,
            backgroundColor: palette.grey[400],

            [`&.${switchClasses.checked}`]: {
              color: palette.background.default,
              backgroundColor: "transparent",
              [`& + .${switchClasses.track}`]: {
                backgroundColor: palette.primary.main,
                opacity: 1,
                border: "none",
              },
              [`&.${switchClasses.disabled}`]: {
                color: palette.background.default,
                opacity: 1,
                backgroundColor: "transparent",

                [`& + .${switchClasses.track}`]: {
                  backgroundColor: palette.primary.main,
                  color: "red",
                  opacity: 1,
                  border: "none",
                },
              },
            },
            [`&.${switchClasses.disabled}`]: {
              color: palette.background.default,
              opacity: 1,
              backgroundColor: "transparent",

              [`& + .${switchClasses.track}`]: {
                backgroundColor: palette.grey[400],
                color: "red",
                opacity: 1,
                border: "none",
              },
            },
          },
          track: {
            backgroundColor: palette.grey[400],
            borderRadius: 26 / 2,
            opacity: 1,
          },
        },
      },
    },
  });
};
