import React from "react";
import { useNavigate } from "react-router-dom";

interface ProductInfoComponentCardProps {
  id: string;
  identifier: string;
  name: string;
  quantity: number;
}

const ProductInfoComponentCard = ({
  id,
  identifier,
  name,
  quantity,
}: ProductInfoComponentCardProps) => {
  const navigate = useNavigate();
  const handleMoreInfoClick = () => {
    navigate("/component-info/" + id);
  };
  return (
    <div
      id="toast-default"
      className="flex flex-col my-4 w-full mx-2 items-center p-4 text-gray-500 bg-gray-500 rounded-lg shadow-sm dark:text-gray-400 dark:bg-gray-800"
      role="alert"
    >
      <div className="w-full flex flex-row justify-between">
        <div className="flex flex-row w-full items-center">
          <div className="inline-flex items-center justify-center shrink-0 w-10 h-10 text-white bg-slate-700 rounded-lg dark:bg-blue-800 dark:text-blue-200">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth="1.5"
              stroke="currentColor"
              className="size-7"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M19.5 14.25v-2.625a3.375 3.375 0 0 0-3.375-3.375h-1.5A1.125 1.125 0 0 1 13.5 7.125v-1.5a3.375 3.375 0 0 0-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 0 0-9-9Z"
              />
            </svg>

            <span className="sr-only">Fire icon</span>
          </div>

          <div className="ms-3 flex flex-row items-center border-l-2 pl-2 text-sm font-normal text-gray-300">
            Identifier:
            <span className="ms-2 font-medium text-base text-white">
              {identifier}
            </span>
          </div>
          <div className="ms-3 flex flex-row items-center border-l-2 pl-2 text-sm font-normal text-gray-300">
            Name:
            <span className="ms-2 font-medium text-base text-white">
              {name}
            </span>
          </div>
          <div className="ms-3 flex flex-row items-center border-l-2 pl-2 text-sm font-normal text-gray-300">
            Quantity:
            <span className="ms-2 font-medium text-base text-white">
              {quantity}
            </span>
          </div>
        </div>
        <button
          onClick={handleMoreInfoClick}
          className="text-white bg-green-600 text-sm font-medium hover:bg-green-700 px-4 py-2 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 transition"
        >
          More&nbsp;info
        </button>
      </div>
    </div>
  );
};

export default ProductInfoComponentCard;
