import React, { useEffect, useState } from "react";
import { ExporterExportInvolvementResponse } from "../../../../../model/exporter/ExporterExportInvolvementResponse";
import { ExporterProductInvolvementResponse } from "../../../../../model/exporter/ExporterProductInvolvementResponse";
import ProviderInvolvementPieChart from "../provider/ProviderInvolvementPieChart";
interface ExporterInvolvementCarouselProps {
  export: ExporterExportInvolvementResponse[];
  components: ExporterProductInvolvementResponse[];
}

const ExporterInvolvementCarousel = ({
  export: invoice,
  components,
}: ExporterInvolvementCarouselProps) => {
  const [page, setPage] = useState(0);
  const [displayedProvidersByInvoice, setDisplayedProvidersByInvoice] =
    useState<ExporterExportInvolvementResponse[]>([]);
  const [displayedProvidersByComponents, setDisplayedProvidersByComponents] =
    useState<ExporterProductInvolvementResponse[]>([]);
  useEffect(() => {
    setDisplayedProvidersByInvoice(invoice);
    setDisplayedProvidersByComponents(components);
  }, [invoice, components]);
  const handleNext = () => {
    setPage((page + 1) % 2);
  };
  const handlePrevious = () => {
    setPage(Math.abs(page - 1) % 2);
  };
  return (
    <div
      id="default-carousel"
      className="relative w-full h-full"
      data-carousel="slide"
    >
      <div className="relative h-full overflow-hidden rounded-lg">
        <div
          className={`duration-700 h-full ease-in-out ${
            page === 0 ? "" : "hidden"
          }`}
          data-carousel-item
        >
          <div className="flex flex-col h-full w-full">
            <span className="text-base font-medium w-full flex justify-start">
              Import involvement
            </span>
            <div className="w-full h-full flex justify-center p-4 pt-2 text-white text-lg font-medium">
              <ProviderInvolvementPieChart
                dataKey="invoiceCount"
                data={displayedProvidersByInvoice}
              />
            </div>
          </div>
        </div>
        <div
          className={`duration-700 h-full ease-in-out ${
            page === 1 ? "" : "hidden"
          }`}
        >
          <div className="flex flex-col h-full w-full">
            <span className="text-base font-medium w-full flex justify-start">
              Component involvement
            </span>
            <div className="w-full h-full flex justify-center p-4 pt-2 text-white text-lg font-medium">
              <ProviderInvolvementPieChart
                dataKey="componentCount"
                data={displayedProvidersByComponents}
              />
            </div>
          </div>
        </div>
      </div>
      <div className="absolute z-30 flex -translate-x-1/2 bottom-5 left-1/2 space-x-3 rtl:space-x-reverse">
        <button
          type="button"
          className="w-3 h-3 rounded-full"
          aria-current="true"
          aria-label="Slide 1"
          data-carousel-slide-to="0"
        ></button>
        <button
          type="button"
          className="w-3 h-3 rounded-full"
          aria-current="false"
          aria-label="Slide 2"
          data-carousel-slide-to="1"
        ></button>
        <button
          type="button"
          className="w-3 h-3 rounded-full"
          aria-current="false"
          aria-label="Slide 3"
          data-carousel-slide-to="2"
        ></button>
        <button
          type="button"
          className="w-3 h-3 rounded-full"
          aria-current="false"
          aria-label="Slide 4"
          data-carousel-slide-to="3"
        ></button>
        <button
          type="button"
          className="w-3 h-3 rounded-full"
          aria-current="false"
          aria-label="Slide 5"
          data-carousel-slide-to="4"
        ></button>
      </div>
      <button
        type="button"
        onClick={handlePrevious}
        className="absolute top-0 start-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
        data-carousel-prev
      >
        <span className="inline-flex items-center justify-center w-10 h-10 rounded-full bg-white/30 dark:bg-gray-800/30 group-hover:bg-white/50 dark:group-hover:bg-gray-800/60 group-focus:ring-4 group-focus:ring-white dark:group-focus:ring-gray-800/70 group-focus:outline-none">
          <svg
            className="w-4 h-4 text-white dark:text-gray-800 rtl:rotate-180"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 6 10"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M5 1 1 5l4 4"
            />
          </svg>
          <span className="sr-only">Previous</span>
        </span>
      </button>
      <button
        type="button"
        onClick={handleNext}
        className="absolute top-0 end-0 z-30 flex items-center justify-center h-full px-4 cursor-pointer group focus:outline-none"
        data-carousel-next
      >
        <span className="inline-flex items-center justify-center w-10 h-10 rounded-full bg-white/30 dark:bg-gray-800/30 group-hover:bg-white/50 dark:group-hover:bg-gray-800/60 group-focus:ring-4 group-focus:ring-white dark:group-focus:ring-gray-800/70 group-focus:outline-none">
          <svg
            className="w-4 h-4 text-white dark:text-gray-800 rtl:rotate-180"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 6 10"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="m1 9 4-4-4-4"
            />
          </svg>
          <span className="sr-only">Next</span>
        </span>
      </button>
    </div>
  );
};

export default ExporterInvolvementCarousel;
