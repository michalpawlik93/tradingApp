import React from "react";
import { Box } from "@mui/material";

interface PageItemsWrapperProps {
  children: React.ReactNode;
  className?: string;
}
export const PageItemsWrapper = ({
  children,
  className,
}: PageItemsWrapperProps): React.ReactElement => (
  <Box px={4} className={className}>
    {children}
  </Box>
);
