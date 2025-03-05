import React, { ReactNode, useState } from "react";
import AppNavbar from "../Navbar/AppNavbar";
import Navigation from "../Navigation/Navigation";
import { motion, Variants } from "framer-motion";
import { transitions } from "../AnimatedRoutes/TransitionVariants";

interface ContentContainerProps {
  children?: ReactNode;
}

const ContentContainer = ({ children }: ContentContainerProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const toggleOffCanvas = () => {
    console.log(isOpen);
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
        style={{ height: "100%" }}
      >
        {children}
      </motion.div>
      <Navigation
        isOpen={isOpen}
        toggleOffCanvas={toggleOffCanvas}
      ></Navigation>
    </>
  );
};

export default ContentContainer;
