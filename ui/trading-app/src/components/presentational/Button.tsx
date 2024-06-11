import React, { memo } from "react";
import { Button } from "@mui/material";

export interface CommonButtonProps {
  text: string;
  disabled?: boolean;
  onClick?: React.MouseEventHandler;
  secondary?: boolean;
  startIcon?: React.ReactNode;
  size?: "large" | "small" | "medium";
  type?: "button" | "submit" | "reset";
}

const CommonButtonInternal = ({
  onClick,
  disabled,
  text,
  secondary,
  startIcon,
  type = "button",
  size = "medium",
}: CommonButtonProps): JSX.Element => {
  const variant = secondary ? "outlined" : "contained";

  return (
    <Button
      type={type}
      variant={variant}
      color="primary"
      onClick={onClick}
      disabled={disabled}
      startIcon={startIcon}
      size={size}
    >
      {text}
    </Button>
  );
};

export const CommonButton = memo(CommonButtonInternal);
