import { ReactNode, useState } from "react";
import styles from "./Layout.module.css";
import Navigation from "../Navigation/Navigation";
import AppHeader from "../AppHeader/AppHeader";
import AppNavbar from "../Navbar/AppNavbar";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  return (
    <div className={styles.layout}>
      <>
        <AppHeader></AppHeader>
        <div className={`${styles.mainContent}`}>{children}</div>
      </>
    </div>
  );
};

export default Layout;
