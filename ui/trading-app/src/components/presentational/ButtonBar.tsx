import React, { FC, memo } from "react";
import { CardActions } from "@mui/material";

export interface ButtonBarProps {
  children: React.ReactNode;
  className?: string;
}

const ButtonBarInternal: FC<ButtonBarProps> = ({
  children,
  className,
}) => {
  return (
    <CardActions disableSpacing className={className}>
      {children}
    </CardActions>
  );
};

export const ButtonBar = memo(ButtonBarInternal);
