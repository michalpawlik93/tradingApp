import { FC } from "react";
import { CardHeader, Typography, Theme } from "@mui/material";
import { css } from "@emotion/react";

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

export const PageHeader: FC<PageHeaderProps> = ({ title, description }) => {
  const titleComponent = <Typography variant={"h2"}>{title}</Typography>;
  return (
    <CardHeader
      css={pageHeaderCss.cardHeader}
      title={titleComponent}
      subheader={description}
      subheaderTypographyProps={{ css: pageHeaderCss.cardDescription }}
    />
  );
};
