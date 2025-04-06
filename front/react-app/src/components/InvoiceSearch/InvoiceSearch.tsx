import React, { useEffect, useState } from "react";
import InvoiceSearchCard from "./Cards/InvoiceSearchCard";
import { InvoiceSearchResponse } from "../../model/invoice/InvoiceSearchResponse";
import { InvoiceService } from "../../services/InvoiceService";
import InvoiceSearchPagination from "./InvoiceSearchPagination";

const InvoiceSearch = () => {
  const [invoices, setInvoices] = useState<InvoiceSearchResponse[]>([]);
  useEffect(() => {
    InvoiceService.findFiltered({
      date: null,
      id: null,
      pageNumber: 1,
      pageSize: 1,
    }).then((response) => {
      console.log(response.data);
      setInvoices(response.data.responses.items);
    });
  }, []);
  return (
    <div className="h-screen r w-full p-8 ">
      <InvoiceSearchPagination></InvoiceSearchPagination>
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
