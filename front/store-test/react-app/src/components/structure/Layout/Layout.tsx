import { useState } from "react";
import styles from "./Layout.module.css";
import Navigation from "../Navigation/Navigation";
import AppHeader from "../AppHeader/AppHeader";

const Layout = () => {
  const [isOpen, setIsOpen] = useState(false);
  const toggleOffCanvas = () => {
    setIsOpen(!isOpen);
  };
  return (
    <div className={styles.layout}>
      <>
        <AppHeader></AppHeader>
        <nav
          className={`navbar navbar-expand-lg navbar-dark bg-dark ${styles.myNavbar}`}
        >
          <a
            className="navbar-brand"
            href="#offcanvasExample"
            data-bs-toggle="offcanvas"
            onClick={toggleOffCanvas}
          >
            <span className="navbar-toggler-icon"></span>
          </a>
        </nav>
        <Navigation
          isOpen={isOpen}
          toggleOffCanvas={toggleOffCanvas}
        ></Navigation>
      </>
    </div>
  );
};

export default Layout;
