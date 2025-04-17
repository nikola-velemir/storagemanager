import { useNavigate } from "react-router-dom";
import { ProductInfoExporterResponse } from "../../../../../model/product/ProductInfoExporterResponse";

interface ProductInfoExportCardProps {
  id: string;
  date: string;
  exporter: ProductInfoExporterResponse;
}

const ProductInfoExportCard = ({
  id,
  date,
  exporter,
}: ProductInfoExportCardProps) => {
  const navigate = useNavigate();
  const handleExporterMoreInfoClick = () => navigate("/exporter-info");

  const handleInvoiceMoreInfoClick = () => navigate("/invoice-info");

  return (
    <div
      id="toast-default"
      className="flex flex-col my-4 w-full mx-2 items-center p-4 text-gray-500 bg-gray-500 rounded-lg shadow-sm dark:text-gray-400 dark:bg-gray-800"
      role="alert"
    >
      <div className="w-full flex flex-row justify-between">
        <div className="flex flex-row w-full">
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
          <div className="ms-3 flex flex-row items-center text-sm font-normal text-gray-300">
            Id:
            <span className="ms-2 font-medium text-base text-white">{id}</span>
          </div>
          <div className="ms-3 flex flex-row items-center border-l-2 pl-2 text-sm font-normal text-gray-300">
            Name:
            <span className="ms-2 font-medium text-base text-white">
              {date}
            </span>
          </div>
        </div>
        <button
          onClick={handleInvoiceMoreInfoClick}
          className="text-white bg-green-600 text-sm font-medium hover:bg-green-700 px-4 py-2 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 transition"
        >
          More&nbsp;info
        </button>
      </div>
      <div className="flex w-full flex-row mt-6">
        <div className="flex w-full flex-row">
          <div className="w-10"></div>
          <div className="w-10 h-10 grid place-items-center">
            <div className="inline-flex items-center justify-center shrink-0 w-8 h-8 text-white bg-slate-700 rounded-lg dark:bg-blue-800 dark:text-blue-200">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth={1.5}
                stroke="currentColor"
                className="size-6"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="M17.982 18.725A7.488 7.488 0 0 0 12 15.75a7.488 7.488 0 0 0-5.982 2.975m11.963 0a9 9 0 1 0-11.963 0m11.963 0A8.966 8.966 0 0 1 12 21a8.966 8.966 0 0 1-5.982-2.275M15 9.75a3 3 0 1 1-6 0 3 3 0 0 1 6 0Z"
                />
              </svg>
              <span className="sr-only">Fire icon</span>
            </div>
          </div>
          <div className="ms-3 flex flex-row items-center text-sm font-normal text-gray-300">
            Name:
            <span className="ms-2 font-medium text-base text-white">
              {exporter.name}
            </span>
          </div>
          <div className="ms-3 flex flex-row items-center border-l-2 pl-2 text-sm font-normal text-gray-300">
            Address:
            <span className="ms-2 font-medium text-base text-white">
              {exporter.address}
            </span>
          </div>
          <div className="ms-3 flex flex-row items-center border-l-2 pl-2 text-sm font-normal text-gray-300">
            Phone number:
            <span className="ms-2 font-medium text-base text-white">
              {exporter.phoneNumber}
            </span>
          </div>
        </div>
        <button
          onClick={handleExporterMoreInfoClick}
          className="text-white bg-green-600 text-sm font-medium hover:bg-green-700 px-4 py-2 rounded-xl focus:outline-none focus:ring-2 focus:ring-green-400 transition"
        >
          More&nbsp;info
        </button>
      </div>
    </div>
  );
};

export default ProductInfoExportCard;
