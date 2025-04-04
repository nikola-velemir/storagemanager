import React from "react";
import InvoiceSearchCardAccordion from "./InvoiceSearchCardAccordion";

const InvoiceSearchCard = () => {
  return (
    <div className="w-full flex flex-col bg-slate-700 border border-gray-200 rounded-lg shadow-sm">
      <div className="w-full flex flex-row justify-between p-8">
        <h5 className="text-2xl font-semibold tracking-tight text-white dark:text-white">
          Need a help in Claim?
        </h5>
        <p className="font-normal text-white dark:text-gray-400">
          Go to this step by step guideline process on how to certify for your
          weekly benefits:
        </p>
      </div>
      <div className="w-full flex flex-col">
        <InvoiceSearchCardAccordion />
      </div>
    </div>
  );
};

export default InvoiceSearchCard;
