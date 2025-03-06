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
      className={`focus:outline-none transition overflow-hidden duration-200 rounded-none mx-0 my-0 text-white bg-transparent ${transitionColor} font-medium text-sm py-1 px-2`}
    >
      {icon}
    </button>
  );
};

export default WindowControllButton;
