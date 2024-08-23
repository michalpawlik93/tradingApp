import { PropsWithChildren, useState } from "react";
import { BaseDialog } from "./BaseDialog";
import { CommonButton } from "./Button";

interface BaseDialogOpenerProps {
  buttonText: string;
  dialogMaxWidth?: "xs" | "sm" | "md" | "lg" | "xl" | false;
  fullWidth?: boolean;
  closeButton?: boolean;
  dialogTitle?: string;
  message?: string;
}

export const BaseDialogOpener = ({
  buttonText,
  children,
  dialogMaxWidth,
  fullWidth,
  closeButton,
  dialogTitle,
  message,
}: PropsWithChildren<BaseDialogOpenerProps>) => {
  const [open, setOpen] = useState(false);

  function handleOpenModal() {
    setOpen(true);
  }

  function handleCloseModal() {
    setOpen(false);
  }
  return (
    <>
      <CommonButton text={buttonText} onClick={handleOpenModal} size="medium" type="button" />
      <BaseDialog
        onClose={handleCloseModal}
        open={open}
        maxWidth={dialogMaxWidth}
        fullWidth={fullWidth}
        closeButton={closeButton}
        title={dialogTitle}
        message={message}
      >
        {children}
      </BaseDialog>
    </>
  );
};
