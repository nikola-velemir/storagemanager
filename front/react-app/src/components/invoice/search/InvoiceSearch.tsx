import { useEffect, useState } from "react";
import InvoiceSearchCard from "./cards/InvoiceSearchCard";
import { InvoiceSearchResponse } from "../../../model/invoice/InvoiceSearchResponse";
import { InvoiceService } from "../../../services/InvoiceService";
import InvoiceSearchPagination from "../../common/inputs/Paginator";
import DatePickerComponent from "../../common/inputs/DatePickerComponent";
import { ProviderService } from "../../../services/ProviderService";
import { ProviderGetResponse } from "../../../model/provider/ProviderGetResponse";
import SearchBox from "../../common/inputs/SearchBox";
import SelectProviderBox from "./cards/SelectProviderBox";

export const convertDateToString = (date: Date | null) => {
  if (!date) {
    return null;
  }
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};

const InvoiceSearch = () => {
  const [invoices, setInvoices] = useState<InvoiceSearchResponse[]>([]);
  const [totalItems, setTotalItems] = useState<number>(0);
  const [pageSize, setPageSize] = useState(5);
  const [pageNumber, setPageNumber] = useState(1);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [providers, setProviders] = useState<ProviderGetResponse[]>([]);
  const [selectedProvider, setSelectedProvider] =
    useState<ProviderGetResponse | null>(null);
  const [componentInfo, setComponentInfo] = useState<string | null>(null);
  const fetchInvoices = () => {
    InvoiceService.findFiltered({
      componentInfo: componentInfo,
      date: convertDateToString(selectedDate),
      id: selectedProvider ? selectedProvider.id : null,
      pageNumber: pageNumber,
      pageSize: pageSize,
    }).then((response) => {
      setInvoices(response.data.items);
      setTotalItems(response.data.totalCount);
    });
  };
  const fetchProviders = () => {
    ProviderService.findAll().then((response) => {
      setProviders(response.data.providers);
    });
  };
  useEffect(() => {
    fetchProviders();
  }, []);
  useEffect(() => {
    fetchInvoices();
  }, [pageSize, pageNumber, selectedDate, selectedProvider, componentInfo]);
  const handlePageSizeChange = (newPageSize: number) => {
    setPageSize(newPageSize);
    setPageNumber(1);
  };
  const handlePageNumberChange = (newPageNumber: number) => {
    setPageNumber(newPageNumber);
  };
  const handleDateChange = (e: Date | null) => {
    setSelectedDate(e);
    setPageNumber(1);
  };
  const handleProviderChange = (p: ProviderGetResponse | null) => {
    setSelectedProvider(p);
    setPageNumber(1);
  };
  const handleInputChange = (text: string) => {
    setComponentInfo(text.trim().length > 0 ? text.trim() : null);
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full pb-2 gap-4 flex flex-row justify-center items-end">
        <SearchBox onInput={handleInputChange} />
        <DatePickerComponent onDateChange={handleDateChange} />
        <InvoiceSearchPagination
          totalItems={totalItems}
          onPageNumberChange={handlePageNumberChange}
          onPageSizeChange={handlePageSizeChange}
        />
        <SelectProviderBox
          emitProviderChange={handleProviderChange}
          providers={providers}
        />
      </div>

      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {invoices.map((invoice: InvoiceSearchResponse) => {
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
