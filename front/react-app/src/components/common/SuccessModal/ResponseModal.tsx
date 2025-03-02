import { ReactNode } from "react";
import styles from "./ResponseModal.module.css";

interface ResponseModalProps {
  children: ReactNode;
}

const ResponseModal = ({ children }: ResponseModalProps) => {
  return (
    <div
      className={`modal fade show ${styles.successModal}`}
      style={{ display: "block" }}
      data-bs-backdrop="static"
      data-bs-keyboard="false"
      tabIndex={-1}
      aria-labelledby="staticBackdropLabel"
      aria-hidden="true"
    >
      <div className="modal-dialog  modal-dialog-centered">
        <div className="modal-content">
          <div className={`modal-body ${styles.modalBody}`}>
            {/*  */}
            {children}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ResponseModal;
