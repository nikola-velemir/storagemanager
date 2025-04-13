import { ReactNode } from "react";
import styles from "./Layout.module.css";
import AppHeader from "../Window/AppHeader/AppHeader";
import { motion } from "framer-motion";
import { transitions } from "../AnimatedRoutes/TransitionVariants";
import { ToastContainer } from "react-toastify";

interface LayoutProps {
  children: ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  return (
    <div className={styles.layout}>
      <>
        <AppHeader></AppHeader>
        <ToastContainer position="bottom-right" autoClose={3000} />
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
