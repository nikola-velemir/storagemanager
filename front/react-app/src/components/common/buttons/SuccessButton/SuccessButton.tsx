import React from "react";

interface SuccessButtonProps {
  text?: string;
  onClick?: any;
}

const SuccessButton = ({ text, onClick }: SuccessButtonProps) => {
  return (
    <button
      onClick={onClick}
      type="button"
      className="text-white w-64 text-lg bg-gradient-to-r from-green-400 via-green-500 to-green-600 hover:bg-gradient-to-br focus:ring-4 focus:outline-none focus:ring-green-300 dark:focus:ring-green-800 font-medium rounded-lg  px-5 py-2.5 text-center me-2 mb-2"
    >
      {text}
    </button>
  );
};

export default SuccessButton;
