import { ReactNode, useState } from "react";
import styles from "./Layout.module.css";
import Navigation from "../Navigation/Navigation";
import AppHeader from "../AppHeader/AppHeader";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
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
        <div className={`${styles.mainContent}`}>{children}</div>
        <Navigation
          isOpen={isOpen}
          toggleOffCanvas={toggleOffCanvas}
        ></Navigation>
      </>
    </div>
  );
};

export default Layout;
