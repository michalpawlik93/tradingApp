import React, { PropsWithChildren } from "react";
import { PageHeader, PageHeaderProps } from "./PageHeader";
import { css } from "@emotion/react";
import { ButtonBar } from "./ButtonBar";
import { Paper, Box } from "@mui/material";

export interface FenPageProps {
  bottomButtons?: React.ReactNode;
  headerProps?: PageHeaderProps;
}

export const Page = (props: PropsWithChildren<FenPageProps>): JSX.Element => {
  const { children, headerProps, bottomButtons } = props;

  return (
    <div css={PageCss.box}>
      <Paper elevation={1}>
        {headerProps && (
          <div css={PageCss.header}>
            <PageHeader {...headerProps} />
          </div>
        )}
        <Box px={4} css={PageCss.box}>
          {children}
        </Box>
        {bottomButtons && (
          <Box px={4} css={PageCss.box}>
            <ButtonBar css={PageCss.buttonBar}>{bottomButtons}</ButtonBar>
          </Box>
        )}
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
  buttonBar: () =>
    css({
      paddingTop: "2rem",
    }),
};
