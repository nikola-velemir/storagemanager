import React, { useEffect, useState } from "react";
import InvoiceSearchCard from "./Cards/InvoiceSearchCard";
import { InvoiceSearchResponse } from "../../model/invoice/InvoiceSearchResponse";
import { InvoiceService } from "../../services/InvoiceService";

const InvoiceSearch = () => {
  const [invoices, setInvoices] = useState<InvoiceSearchResponse[]>([]);
  useEffect(() => {
    InvoiceService.findAll().then((response) => {
      setInvoices(response.data.responses);
    });
  }, []);
  return (
    <div className="h-screen w-full p-8 ">
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {invoices.map((invoice: InvoiceSearchResponse) => {
          console.log(invoice);
          return (
            <InvoiceSearchCard
              key={invoice.id}
              id={invoice.id}
              components={invoice.components}
              date={invoice.date}
              providerName={invoice.provider.name}
            />
          );
        })}
      </div>
    </div>
  );
};

export default InvoiceSearch;
