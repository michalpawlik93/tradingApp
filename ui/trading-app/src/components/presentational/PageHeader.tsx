import { css } from "@emotion/react";
import { CardHeader, Theme, Typography } from "@mui/material";
import { useTheme } from "@mui/system";

export interface PageHeaderProps {
  description?: string;
  title: string;
}

const pageHeaderCss = {
  cardHeader: (theme: Theme) =>
    css({
      paddingLeft: theme.spacing(3),
      paddingTop: theme.spacing(2),
      paddingBottom: theme.spacing(2),
      paddingRight: theme.spacing(3),
      width: "100%",
    }),
  cardDescription: (theme: Theme) =>
    css({
      marginTop: theme.spacing(3),
      marginBottom: theme.spacing(2),
    }),
};

export const PageHeader = ({ title, description }: PageHeaderProps) => {
  const titleComponent = <Typography variant={"h2"}>{title}</Typography>;
  const theme: Theme = useTheme();
  return (
    <CardHeader
      css={pageHeaderCss.cardHeader(theme)}
      title={titleComponent}
      subheader={description}
      subheaderTypographyProps={{ css: pageHeaderCss.cardDescription(theme) }}
    />
  );
};
