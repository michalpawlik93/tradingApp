import React, { PropsWithChildren } from "react";
import { css } from "@emotion/react";
import { Box, Paper } from "@mui/material";
import { ButtonBar } from "./ButtonBar";
import { PageHeader, PageHeaderProps } from "./PageHeader";

export interface FenPageProps {
  topButtons?: React.ReactNode;
  headerProps?: PageHeaderProps;
}

export const Page = (props: PropsWithChildren<FenPageProps>): JSX.Element => {
  const { children, headerProps, topButtons } = props;

  return (
    <div css={PageCss.box}>
      <Paper elevation={1}>
        {headerProps && (
          <div css={PageCss.header}>
            <PageHeader {...headerProps} />
          </div>
        )}
        {topButtons && (
          <Box px={4} css={PageCss.box}>
            <ButtonBar>{topButtons}</ButtonBar>
          </Box>
        )}
        <Box px={4} css={PageCss.box}>
          {children}
        </Box>
      </Paper>
    </div>
  );
};

const PageCss = {
  header: () =>
    css({
      padding: "0 2rem",
      paddingBottom: "1rem",
    }),
  box: () =>
    css({
      paddingTop: "2rem",
      paddingRight: "2rem",
      paddingLeft: "2rem",
      paddingBottom: "2rem",
    }),
};
