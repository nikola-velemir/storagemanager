import React, { useState } from "react";
import styles from "./Navigation.module.css";

interface OffcanvasProps {
  isOpen: boolean;
  toggleOffCanvas: () => void;
}

const Navigation = ({ isOpen, toggleOffCanvas }: OffcanvasProps) => {
  return (
    <div
      className={`offcanvas offcanvas-start ${isOpen ? "show" : ""}`}
      tabIndex={-1}
      id="offcanvasExample"
      aria-labelledby="offcanvasExampleLabel"
    >
      <div className="offcanvas-header">
        <button
          type="button"
          className="btn-close"
          data-bs-dismiss="offcanvas"
          onClick={toggleOffCanvas}
          aria-label="Close"
        ></button>
      </div>
      <div className="offcanvas-body">
        <ul className={`list-group ${styles.navigationList}`}>
          <li className="list-group-item">An item</li>
          <li className="list-group-item">A second item</li>
          <li className="list-group-item">A third item</li>
          <li className="list-group-item">A fourth item</li>
          <li className="list-group-item">And a fifth one</li>
        </ul>
      </div>
    </div>
  );
};

export default Navigation;
