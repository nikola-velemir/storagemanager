import { ReactNode, useState } from "react";
import styles from "./Layout.module.css";
import Navigation from "../Navigation/Navigation";
import AppHeader from "../AppHeader/AppHeader";
import AppNavbar from "../Navbar/AppNavbar";
import { motion } from "framer-motion";
import { transitions } from "../AnimatedRoutes/TransitionVariants";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  return (
    <div className={styles.layout}>
      <>
        <AppHeader></AppHeader>
        <motion.div
          variants={transitions}
          initial="initial"
          exit="exit"
          animate="animate"
          className={`${styles.mainContent}`}
        >
          {children}
        </motion.div>
      </>
    </div>
  );
};

export default Layout;
