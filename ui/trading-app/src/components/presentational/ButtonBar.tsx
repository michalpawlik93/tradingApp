import React, { FC, memo } from "react";
import ButtonGroup from "@mui/material/ButtonGroup";

export interface ButtonBarProps {
  children: React.ReactNode;
}

const ButtonBarInternal: FC<ButtonBarProps> = ({ children }) => {
  return (
    <ButtonGroup variant="outlined" aria-label="outlined button group">
      {children}
    </ButtonGroup>
  );
};

export const ButtonBar = memo(ButtonBarInternal);
