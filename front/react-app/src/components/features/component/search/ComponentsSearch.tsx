import { useEffect, useState } from "react";
import { ProviderGetResponse } from "../../../../model/provider/ProviderGetResponse";
import { MechanicalComponentService } from "../../../../services/MechanicalComponentService";
import Paginator from "../../../common/inputs/Paginator";
import SearchBox from "../../../common/inputs/SearchBox";
import ComponentCard from "./cards/ComponentCard";
import { MechanicalComponentSearchResponse } from "../../../../model/components/search/MechanicalComponentSearchResponse";
import { ProviderService } from "../../../../services/businessPartner/ProviderService";
import SelectBusinessPartnerBox from "../../invoice/search/SelectPartnerSearchBox";

export const convertDateToString = (date: Date | null) => {
  if (!date) {
    return null;
  }
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};
const ComponentsSearch = () => {
  const [components, setComponents] = useState<
    MechanicalComponentSearchResponse[]
  >([]);
  const [selectedProvider, setSelectedProvider] =
    useState<ProviderGetResponse | null>(null);
  const [providers, setProviders] = useState<ProviderGetResponse[]>([]);
  const [totalItems, setTotalItems] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [searchText, setSearchText] = useState<string | null>(null);
  const handleProviderFetch = async () => {
    const res = await ProviderService.findAll();
    return { data: res.data.providers };
  };
  useEffect(() => {
    MechanicalComponentService.findFiltered({
      componentInfo: searchText,
      providerId: selectedProvider ? selectedProvider.id : null,
      pageNumber: pageNumber,
      pageSize: pageSize,
    }).then((response) => {
      setComponents(response.data.items);
      setTotalItems(response.data.totalCount);
    });
  }, [pageNumber, pageSize, selectedProvider, searchText]);
  const handlePageSizeChange = (n: number) => {
    setPageSize(n);
  };
  const handlePageNumberChange = (n: number) => {
    setPageNumber(n);
  };
  const handleProviderChange = (p: ProviderGetResponse | null) => {
    setSelectedProvider(p);
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
          placeholderText={"Component info"}
        />
        <Paginator
          totalItems={totalItems}
          onPageSizeChange={handlePageSizeChange}
          onPageNumberChange={handlePageNumberChange}
        />
        <SelectBusinessPartnerBox
          emitPartnerChange={handleProviderChange}
          onFetchPartners={handleProviderFetch}
        />
      </div>
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {components.map((component: MechanicalComponentSearchResponse) => {
          return (
            <ComponentCard
              invoices={component.invoices}
              id={component.id}
              identifier={component.identifier}
              name={component.name}
            />
          );
        })}
      </div>
    </div>
  );
};

export default ComponentsSearch;
