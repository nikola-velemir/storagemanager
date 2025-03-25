import { ReactNode, useState } from "react";
import AppNavbar from "../Navigation/Navbar/AppNavbar";
import { motion } from "framer-motion";
import { transitions } from "../AnimatedRoutes/TransitionVariants";
import Sidenav from "../Navigation/Sidenav/Sidenav";

interface ContentContainerProps {
  children?: ReactNode;
}

const ContentContainer = ({ children }: ContentContainerProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const toggleOffCanvas = () => {
    setIsOpen(!isOpen);
  };
  return (
    <>
      <AppNavbar toggleDrawer={toggleOffCanvas}></AppNavbar>{" "}
      <motion.div
        variants={transitions}
        initial="initial"
        exit="exit"
        animate="animate"
        className="bg-slate-800"
        style={{ flexGrow: 1 }}
      >
        {children}
      </motion.div>
      <Sidenav isOpen={isOpen} toggleOffCanvas={toggleOffCanvas}></Sidenav>
    </>
  );
};

export default ContentContainer;
