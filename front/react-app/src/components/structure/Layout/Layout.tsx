import { ReactNode, useState } from "react";
import styles from "./Layout.module.css";
import Navigation from "../Navigation/Navigation";
import AppHeader from "../AppHeader/AppHeader";
import AppNavbar from "../Navbar/AppNavbar";
import { motion } from "framer-motion";

interface LayoutProps {
  children: ReactNode;
}
const pageVariants = {
  initial: { opacity: 0, x: -50 },
  animate: { opacity: 1, x: 0, transition: { duration: 0.5 } },
  exit: { opacity: 0, x: 50, transition: { duration: 0.3 } },
};
const Layout = ({ children }: LayoutProps) => {
  return (
    <div className={styles.layout}>
      <>
        <AppHeader></AppHeader>

        <motion.div
          className={`${styles.mainContent}`}
          initial="initial"
          animate="animate"
          exit="exit"
          variants={pageVariants}
        >
          {children}
        </motion.div>
      </>
    </div>
  );
};

export default Layout;
