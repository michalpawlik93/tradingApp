import { PropsWithChildren } from "react";
import { css } from "@emotion/react";
import CloseIcon from "@mui/icons-material/Close";
import { Dialog, DialogContent, DialogTitle, IconButton } from "@mui/material";

interface BaseDialogProps {
  onClose: () => void;
  open: boolean;
  maxWidth?: "xs" | "sm" | "md" | "lg" | "xl" | false;
  fullWidth?: boolean;
  message?: string;
  scroll?: "paper" | "body";
  closeButton?: boolean;
  title?: string;
}

export const BaseDialog = ({
  title,
  onClose,
  open,
  maxWidth,
  fullWidth,
  children,
  message,
  closeButton,
}: PropsWithChildren<BaseDialogProps>) => {
  const baseDialogCss = {
    dialogHeader: () =>
      css({
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        fontSize: "2rem",
        marginTop: "0.3rem",
        marginBottom: "0.6rem",
      }),
    closeBtn: () =>
      css({
        position: "absolute",
        right: "0.5rem",
        top: "0.5rem",
      }),
    dialogContent: () =>
      css({
        padding: "1rem",
      }),
  };

  return (
    <Dialog
      open={open}
      onClose={onClose}
      fullWidth={fullWidth}
      maxWidth={maxWidth}
      data-testid="base-dialog-component"
    >
      <div css={baseDialogCss.closeBtn}>
        {closeButton && (
          <IconButton size="small" data-testid="close-icon-button">
            <CloseIcon onClick={onClose} data-testid="close-icon" />
          </IconButton>
        )}
      </div>
      {title && (
        <div css={baseDialogCss.dialogHeader}>
          <DialogTitle>
            <h2 style={{ textAlign: "center" }}>{title}</h2>
          </DialogTitle>
        </div>
      )}
      <DialogContent css={baseDialogCss.dialogContent}>
        {message && <p>{message}</p>} {children}
      </DialogContent>
    </Dialog>
  );
};
