import React, { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { ExporterService } from "../../../../../services/ExporterService";
import SearchBox from "../../../../common/inputs/SearchBox";
import Paginator from "../../../../common/inputs/Paginator";

interface BusinessPartnetsSearchTabProps<T> {
  onFetchData: (
    searchText: string | null,
    pageNumber: number,
    pageSize: number
  ) => Promise<{ items: T[]; totalItems: number }>;
  CardComponent: React.ComponentType<any>;
  cardPropsMapper: (item: T) => any;
  searchPlaceHolder: string;
}

const BusinessPartnetsSearchTab = <T,>({
  onFetchData,
  CardComponent,
  cardPropsMapper,
  searchPlaceHolder,
}: BusinessPartnetsSearchTabProps<T>) => {
  const [items, setItems] = useState<T[]>([]);
  const [totalItems, setTotalItems] = useState(0);
  const [searchText, setSearchText] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const handleSearchTextChange = (text: string) => {
    if (text.trim().length > 0) setSearchText(text.trim());
    else setSearchText(null);
  };
  useEffect(() => {
    onFetchData(searchText, pageNumber, pageSize)
      .then((res) => {
        setItems(res.items);
        setTotalItems(res.totalItems);
      })
      .catch(() => toast.error("Failed to fetch"));
  }, [searchText, pageNumber, pageSize]);
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full flex flex-row items-end gap-4">
        <SearchBox
          onInput={handleSearchTextChange}
          placeholderText={searchPlaceHolder}
        />
        <Paginator
          totalItems={totalItems}
          onPageSizeChange={setPageSize}
          onPageNumberChange={setPageNumber}
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

export default BusinessPartnetsSearchTab;
