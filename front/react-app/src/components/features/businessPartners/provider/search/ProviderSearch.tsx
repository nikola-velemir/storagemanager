import { useEffect, useState } from "react";
import Paginator from "../../../../common/inputs/Paginator";
import SearchBox from "../../../../common/inputs/SearchBox";
import { ProviderService } from "../../../../../services/ProviderService";
import { ProviderSearchResponse } from "../../../../../model/provider/ProviderSearchResponse";
import ProviderCard from "./cards/ProviderCard";

const ProviderSearch = () => {
  const [providers, setProviders] = useState<ProviderSearchResponse[]>([]);
  const [searchText, setSearchText] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  useEffect(() => {
    ProviderService.findFiltered({
      pageNumber: pageNumber,
      pageSize: pageSize,
      providerName: searchText,
    }).then((response) => {
      setProviders(response.data.items);
    });
  }, [searchText, pageNumber, pageSize]);
  const handlePageSizeChange = (n: number) => {
    setPageSize(n);
  };
  const handlePageNumberChange = (n: number) => {
    setPageNumber(n);
  };
  const handleSearchTextChange = (text: string) => {
    if (text.trim().length > 0) setSearchText(text.trim());
    else setSearchText(null);
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full flex flex-row items-end gap-4">
        <SearchBox
          onInput={handleSearchTextChange}
          placeholderText={"Provider info"}
        />
        <Paginator
          totalItems={15}
          onPageSizeChange={handlePageSizeChange}
          onPageNumberChange={handlePageNumberChange}
        />
      </div>
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {providers.map((provider: ProviderSearchResponse) => {
          return (
            <ProviderCard
              key={provider.id}
              invoices={provider.invoices}
              address={provider.address}
              id={provider.id}
              name={provider.name}
              phoneNumber={provider.phoneNumber}
            />
          );
        })}
      </div>
    </div>
  );
};

export default ProviderSearch;
