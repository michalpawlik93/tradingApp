import React, { memo } from "react";
import { Button, Theme } from "@mui/material";
import { Interpolation } from "@emotion/react";

export interface CommonButtonProps {
  text: string;
  disabled?: boolean;
  onClick?: React.MouseEventHandler;
  css?: Interpolation<Theme>;
  secondary?: boolean;
  startIcon?: React.ReactNode;
  size?: "extra-small" | "small" | "medium";
  type?: "button" | "submit" | "reset";
}

const CommonButtonInternal = ({
  onClick,
  disabled,
  text,
  css,
  secondary,
  startIcon,
  type = "button",
}: CommonButtonProps): JSX.Element => {
  const variant = secondary ? "outlined" : "contained";

  return (
    <Button
      type={type}
      css={css}
      variant={variant}
      color="primary"
      onClick={onClick}
      disabled={disabled}
      startIcon={startIcon}
    >
      {text}
    </Button>
  );
};

export const CommonButton = memo(CommonButtonInternal);
