import React, { useEffect, useState } from "react";

interface InvoiceSearchPaginationProps {
  onPageSizeChange: (pageSize: number) => void;
  onPageNumberChange: (pageNumber: number) => void;
  totalItems: number;
}

const Paginator = ({
  onPageSizeChange,
  onPageNumberChange,
  totalItems,
}: InvoiceSearchPaginationProps) => {
  const pageSizes = [5, 6, 7, 8, 9, 10];
  const [pageSize, setPageSize] = useState(pageSizes[0]);
  const [pageNumber, setPageNumber] = useState(1);
  useEffect(() => {
    onPageNumberChange(pageNumber);
  }, [pageNumber]);
  const handleNext = () => {
    if (pageNumber < Math.ceil(totalItems / pageSize))
      setPageNumber(pageNumber + 1);
  };
  const handlePrevious = () => {
    if (pageNumber - 1 === 0) {
      return;
    }
    setPageNumber(pageNumber - 1);
    onPageNumberChange(pageNumber);
  };
  const handlePageSizeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const newPageSize = parseInt(e?.target.value);
    setPageNumber(1);
    setPageSize(newPageSize);
    onPageSizeChange(newPageSize);
  };
  return (
    <div className="flex flex-col items-center">
      <form className="max-w-sm flex flex-col items-center">
        <label
          htmlFor="countries"
          className="block mb-2 text-sm font-medium text-white"
        >
          Page size
        </label>
        <div className="flex flex-row items-center h-10">
          <button
            onClick={handlePrevious}
            type="button"
            className="h-full py-2.5 px-5 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-l-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-100 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth="1.5"
              stroke="currentColor"
              className="size-4"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M6.75 15.75 3 12m0 0 3.75-3.75M3 12h18"
              />
            </svg>
          </button>
          <select
            onChange={handlePageSizeChange}
            id="pageSize"
            value={pageSize}
            className="h-full bg-gray-50 border border-gray-300 text-gray-900 text-sm focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
          >
            {pageSizes.map((page: number) => {
              return (
                <option key={page} value={page}>
                  {page}
                </option>
              );
            })}
          </select>
          <button
            onClick={handleNext}
            type="button"
            className="h-full py-2.5 px-5 text-sm font-medium text-gray-900 focus:outline-none bg-white rounded-r-lg border border-gray-200 hover:bg-gray-100 hover:text-blue-700 focus:z-10 focus:ring-4 focus:ring-gray-100 dark:focus:ring-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:border-gray-600 dark:hover:text-white dark:hover:bg-gray-700"
          >
            <svg
              xmlns="http://www.w3.org/2000/svg"
              fill="none"
              viewBox="0 0 24 24"
              strokeWidth={1.5}
              stroke="currentColor"
              className="size-4"
            >
              <path
                strokeLinecap="round"
                strokeLinejoin="round"
                d="M17.25 8.25 21 12m0 0-3.75 3.75M21 12H3"
              />
            </svg>
          </button>
        </div>
      </form>
    </div>
  );
};

export default Paginator;
