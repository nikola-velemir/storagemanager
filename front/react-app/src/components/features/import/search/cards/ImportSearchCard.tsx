import React from "react";
import ImportSearchCardAccordion from "./ImportSearchCardAccordion";
import { ImportSearchComponentResponse } from "../../../../../model/invoice/import/ImportSearchComponentResponse";
import { useNavigate } from "react-router-dom";

interface ImportSearchCardProps {
  id: string;
  date: string;
  providerName: string;
  components: ImportSearchComponentResponse[];
}

const ImportSearchCard = ({
  id,
  date,
  providerName,
  components,
}: ImportSearchCardProps) => {
  const navigate = useNavigate();
  const handleMoreInfoClick = () => {
    navigate("/invoice-info/" + id);
  };
  return (
    <div className="w-11/12 my-8 flex flex-col bg-slate-700 border border-gray-200 rounded-xl shadow-sm">
      <div className="w-full flex flex-row justify-between p-8 items-center">
        <h5 className="text-2xl h-full flex items-center font-semibold tracking-tight text-white dark:text-white">
          {id}
        </h5>
        <div>
          <div className="flex w-full flex-col">
            <p className="text-gray-400 text-sm">Date:</p>
            <p className="font-medium ms-4 text-lg text-green-400">{date}</p>
          </div>
          <div className="flex mt-4 w-full flex-col">
            <p className="text-gray-400 text-sm">Provider:</p>
            <p className="ms-4 font-normal text-white dark:text-gray-400">
              {providerName}
            </p>
          </div>

          <button
            type="button"
            onClick={handleMoreInfoClick}
            className="w-full my-8 text-white bg-green-700 hover:bg-green-800 focus:outline-none focus:ring-4 focus:ring-green-300 font-medium rounded-full text-sm px-5 py-2.5 text-center me-2 mb-2 dark:bg-green-600 dark:hover:bg-green-700 dark:focus:ring-green-800"
          >
            More info
          </button>
        </div>
      </div>
      <div className="flex flex-row justify-end px-8"></div>
      <div className="w-full flex flex-col">
        <ImportSearchCardAccordion components={components} />
      </div>
    </div>
  );
};

export default ImportSearchCard;
