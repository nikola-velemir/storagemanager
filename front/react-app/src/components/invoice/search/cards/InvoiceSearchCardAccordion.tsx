import React, { useState } from "react";
import { InvoiceSearchComponentResponse } from "../../../../model/invoice/InvoiceSearchComponentResponse";
import InvoiceSearchComponentItem from "./InvoiceSearchComponentItem";

export interface InvoiceSearchCardAccordionProps {
  components: InvoiceSearchComponentResponse[];
}

const InvoiceSearchCardAccordion = ({
  components,
}: InvoiceSearchCardAccordionProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const toggleOpen = () => {
    setIsOpen(!isOpen);
  };
  return (
    <div id="accordion-collapse" data-accordion="collapse">
      <h2 id="accordion-collapse-heading-1">
        <button
          type="button"
          className="flex items-center justify-between w-full p-5 font-medium rtl:text-right hover:rounded-lg hover:rounded-t-none round text-white hover:text-slate-800 border border-gray-200 border-b-0 border-l-0 border-r-0 hover:bg-gray-100"
          data-accordion-target="#accordion-collapse-body-1"
          aria-expanded="true"
          aria-controls="accordion-collapse-body-1"
          onClick={toggleOpen}
        >
          <span>Components</span>
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
              stroke-linecap="round"
              stroke-linejoin="round"
              stroke-width="2"
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
        {components.map((component: InvoiceSearchComponentResponse) => {
          return (
            <InvoiceSearchComponentItem
              id={component.id}
              name={component.name}
              identifier={component.identifier}
              price={component.price}
              quantity={component.quantity}
              key={component.identifier}
            />
          );
        })}
      </div>
    </div>
  );
};

export default InvoiceSearchCardAccordion;
