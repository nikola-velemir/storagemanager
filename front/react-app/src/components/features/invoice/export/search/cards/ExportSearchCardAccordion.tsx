import { useState } from "react";
import ExportSearchProductItem from "./ExportSearchProductItem";
import { ExportSearchProductResponse } from "../../../../../../model/invoice/export/ExportSearchProductResponse";

interface ExportSearchCardAccordionProps {
  products: ExportSearchProductResponse[];
}

const ExportSearchCardAccordion = ({
  products,
}: ExportSearchCardAccordionProps) => {
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
          <span>Products</span>
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
        {products.map((component: ExportSearchProductResponse) => {
          return (
            <ExportSearchProductItem
              id={component.id}
              identifier={component.identifier}
              name={component.name}
              price={component.price}
              quantity={component.quantity}
            />
          );
        })}
      </div>
    </div>
  );
};

export default ExportSearchCardAccordion;
