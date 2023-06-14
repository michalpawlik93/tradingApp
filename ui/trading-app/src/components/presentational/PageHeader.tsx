import { FC } from "react";
import { CardHeader, Typography, Paper, Theme } from "@mui/material";
import { css } from "@emotion/react";

export interface PageHeaderProps {
  description?: string;
  title: string;
  /** Display without shadow  */
  flat?: boolean;
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

export const PageHeader: FC<PageHeaderProps> = ({
  title,
  description,
  flat,
}) => {
  const titleComponent = <Typography variant={"h2"}>{title}</Typography>;
  return (
    <Paper elevation={flat ? 0 : 1}>
      <CardHeader
        css={pageHeaderCss.cardHeader}
        title={titleComponent}
        subheader={description}
        subheaderTypographyProps={{ css: pageHeaderCss.cardDescription }}
      />
    </Paper>
  );
};
