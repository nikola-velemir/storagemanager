import React, { useState } from "react";
import BusinessPartnersInvoice from "./BusinessPartnersInvoice";
import { useNavigate } from "react-router-dom";

interface ExporterCardAccordionProps<T extends InvoiceLike> {
  items: T[];
}
export interface InvoiceLike {
  id: string;
  dateIssued: string;
}
const BusinessPartnersSearchAccordion = <T extends InvoiceLike>({
  items,
}: ExporterCardAccordionProps<T>) => {
  const navigate = useNavigate();
  const [isOpen, setIsOpen] = useState(false);
  const toggleOpen = () => {
    setIsOpen(!isOpen);
  };
  return (
    <div id="accordion-collapse" data-accordion="collapse">
      <h2 id="accordion-collapse-heading-1">
        <button
          type="button"
          className="flex items-center justify-between w-full p-5 font-medium rtl:text-right hover:rounded-lg hover:rounded-t-none round text-white hover:text-slate-800 border-b-0 border-l-0 border-r-0"
          data-accordion-target="#accordion-collapse-body-1"
          aria-expanded="true"
          aria-controls="accordion-collapse-body-1"
          onClick={toggleOpen}
        >
          <span>Exports</span>
          <svg
            data-accordion-icon
            className="w-3 h-3 rotate-180 shrink-0"
            aria-hidden="true"
            xmlns="http://www.w3.org/2000/svg"
            fill="none"
            viewBox="0 0 10 6"
          >
            <path
              stroke="currentColor"
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth="2"
              d="M9 5 5 1 1 5"
            />
          </svg>
        </button>
      </h2>
      <div
        id="accordion-collapse-body-1"
        className={`${
          isOpen ? `` : `hidden`
        } w-full flex justify-center items-center flex-col`}
        aria-labelledby="accordion-collapse-heading-1"
      >
        {items.map((invoice: T) => {
          return (
            <BusinessPartnersInvoice
              dateIssued={invoice.dateIssued}
              id={invoice.id}
              key={invoice.id}
            />
          );
        })}
      </div>
    </div>
  );
};

export default BusinessPartnersSearchAccordion;
