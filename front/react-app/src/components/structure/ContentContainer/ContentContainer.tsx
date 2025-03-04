import React, { ReactNode, useState } from "react";
import AppNavbar from "../Navbar/AppNavbar";
import Navigation from "../Navigation/Navigation";

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
      <AppNavbar toggleDrawer={toggleOffCanvas}></AppNavbar>
      {children}
      <Navigation
        isOpen={isOpen}
        toggleOffCanvas={toggleOffCanvas}
      ></Navigation>
    </>
  );
};

export default ContentContainer;
