import { JSX, useCallback, useEffect, useState } from "react";
import DatePickerComponent from "../../../common/inputs/DatePickerComponent";
import Paginator from "../../../common/inputs/Paginator";
import SearchBox from "../../../common/inputs/SearchBox";
import SelectBusinessPartnerBox, {
  PartnerLike,
} from "./SelectPartnerSearchBox";
import { toast } from "react-toastify";
import { PaginatedResponse } from "../../../../model/PaginatedResponse";
import InvoiceSearchCard from "./cards/InvoiceSearchCard";

export const convertDateToString = (date: Date | null) => {
  if (!date) {
    return null;
  }
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};

interface InvoiceSearchTabProps<T, F> {
  fetchItems: (
    pageNumber: number,
    pageSize: number,
    searchText: string | null,
    date: Date | null,
    partnerId: string | null
  ) => Promise<{ data: T[]; totalCount: number }>;
  fetchPartners: () => Promise<{ data: F[] }>;
  cardPropsMapper: (item: T) => any;
  CardComponent: React.ComponentType<any>;
}

const InvoiceSearchTab = <T, F extends PartnerLike>({
  fetchItems,
  fetchPartners,
  cardPropsMapper,
  CardComponent,
}: InvoiceSearchTabProps<T, F>) => {
  const [items, setItems] = useState<T[]>([]);
  const [totalItems, setTotalItems] = useState(0);
  const [searchText, setSearchText] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [selectedPartner, setSelectedPartner] = useState<F | null>(null);
  useEffect(() => {
    fetchItems(
      pageNumber,
      pageSize,
      searchText,
      selectedDate,
      selectedPartner ? selectedPartner.id : null
    )
      .then((res) => {
        setItems(res.data);
        setTotalItems(res.totalCount);
      })
      .catch(() => toast.error("Failed to fetch"));
  }, [searchText, pageNumber, pageSize, selectedDate]);
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
  const handleInputChange = (text: string) => {
    setSearchText(text.trim().length > 0 ? text.trim() : null);
  };
  const handlePartnerChange = (partner: F | null) => {
    setSelectedPartner(partner);
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full pb-2 gap-4 flex flex-row justify-center items-end">
        <SearchBox
          onInput={handleInputChange}
          placeholderText="Component info"
        />
        <DatePickerComponent onDateChange={handleDateChange} />
        <Paginator
          totalItems={totalItems}
          onPageNumberChange={handlePageNumberChange}
          onPageSizeChange={handlePageSizeChange}
        />
        <SelectBusinessPartnerBox
          onFetchPartners={fetchPartners}
          emitPartnerChange={handlePartnerChange}
        />
      </div>

      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {items.map((item, index) => (
          <CardComponent
            key={cardPropsMapper(item).id || index}
            {...cardPropsMapper(item)}
          />
        ))}
      </div>
    </div>
  );
};

export default InvoiceSearchTab;
