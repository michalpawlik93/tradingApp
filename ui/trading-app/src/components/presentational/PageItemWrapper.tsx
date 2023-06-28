import React from "react";
import { Box } from "@mui/material";
import styled from "@emotion/styled";
import { useTheme, Theme } from "@mui/material";

interface PageItemsWrapperProps {
  children: React.ReactNode;
}

const StyledPageItemsWrapper = styled(Box)<{ theme: Theme }>`
  padding: 1.25rem;
  background-color: ${(props) => props.theme.palette.common.black};
`;

export const ChartStyledPageItemsWrapper = ({
  children,
}: PageItemsWrapperProps): React.ReactElement => {
  const theme: Theme = useTheme();
  return (
    <StyledPageItemsWrapper theme={theme}>{children}</StyledPageItemsWrapper>
  );
};

export const PageItemsWrapper = ({
  children,
}: PageItemsWrapperProps): React.ReactElement => <Box px={4}>{children}</Box>;
