import React, { ReactNode } from "react";

interface WindowControllButtonProps {
  icon: ReactNode;
  windowHandler: () => void;
  transitionColor: string;
}

const WindowControllButton = ({
  icon,
  windowHandler,
  transitionColor,
}: WindowControllButtonProps) => {
  return (
    <button
      type="button"
      onClick={windowHandler}
      className={`focus:outline-none transition duration-200 rounded-none mx-0 my-0 text-white bg-transparent hover:${transitionColor} font-medium text-sm py-2 px-3`}
    >
      {icon}
    </button>
  );
};

export default WindowControllButton;
