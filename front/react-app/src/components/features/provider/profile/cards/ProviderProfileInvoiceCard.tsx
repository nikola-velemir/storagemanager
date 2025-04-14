import React from "react";
import { useNavigate } from "react-router-dom";
interface ProviderModalInvoiceCardProps {
  id: string;
  dateIssued: string;
}
const ProviderProfileInvoiceCard = ({
  id,
  dateIssued,
}: ProviderModalInvoiceCardProps) => {
  const navigate = useNavigate();
  const handleInvoicePageNavigate = () => {
    navigate("/invoice-info/" + id);
  };
  return (
    <div
      id="toast-default"
      className="flex flex-col mt-4 w-full items-center p-4 text-gray-500 bg-gray-600 rounded-lg shadow-sm dark:text-gray-400 dark:bg-gray-800"
      role="alert"
    >
      <div className="flex w-full flex-row justify-center items-center">
        <div className="flex flex-row w-full">
          <div className="bg-slate-800 rounded-xl p-2">
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth="1.5"
              stroke="currentColor"
              className="size-12 text-white"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M19.5 14.25v-2.625a3.375 3.375 0 0 0-3.375-3.375h-1.5A1.125 1.125 0 0 1 13.5 7.125v-1.5a3.375 3.375 0 0 0-3.375-3.375H8.25m0 12.75h7.5m-7.5 3H12M10.5 2.25H5.625c-.621 0-1.125.504-1.125 1.125v17.25c0 .621.504 1.125 1.125 1.125h12.75c.621 0 1.125-.504 1.125-1.125V11.25a9 9 0 0 0-9-9Z"
              />
            </svg>

            <span className="sr-only">Fire icon</span>
          </div>
          <div className="flex flex-row gap-8">
            <div className="ms-3 flex flex-row items-center text-sm font-normal text-gray-300">
              <span className="font-light">Id:</span>
              <span className="ms-2 font-medium text-base text-white">
                {id}
              </span>
            </div>
            <div className="ms-3 flex flex-row items-center text-sm font-normal text-gray-300">
              <span className="font-light">Date Issued:</span>
              <span className="ms-2 font-medium text-base text-white">
                {dateIssued}
              </span>
            </div>
          </div>
        </div>
        <button
          onClick={handleInvoicePageNavigate}
          className="h-fit text-white bg-green-600 text-sm font-medium hover:bg-green-700 px-4 py-2 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 transition"
        >
          More&nbsp;info
        </button>
      </div>
    </div>
  );
};

export default ProviderProfileInvoiceCard;
