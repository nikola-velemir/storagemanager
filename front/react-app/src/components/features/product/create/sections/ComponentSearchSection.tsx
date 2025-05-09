import { useEffect, useState } from "react";
import { ProviderGetResponse } from "../../../../../model/provider/ProviderGetResponse";
import { MechanicalComponentService } from "../../../../../services/MechanicalComponentService";
import Paginator from "../../../../common/inputs/Paginator";
import SearchBox from "../../../../common/inputs/SearchBox";
import ComponentSearchSectionCard from "../cards/ComponentSearchSectionCard";
import { ProviderService } from "../../../../../services/businessPartner/ProviderService";
import SelectBusinessPartnerBox from "../../../invoice/search/SelectPartnerSearchBox";
import { MechanicalComponentWithQuantitySearchResponse } from "../../../../../model/components/search/MechanicalComponentWithQuantitySearchResponse";

interface ComponentSearchSectionProps {
  emitComponent: (
    component: MechanicalComponentWithQuantitySearchResponse | null
  ) => void;
}

const ComponentSearchSection = ({
  emitComponent,
}: ComponentSearchSectionProps) => {
  const fetchProviders = async () => {
    const res = await ProviderService.findAll();
    return { data: res.data.providers };
  };
  const handleComponentButtonClick = (id: string) => {
    const foundComponent = components.find((i) => i.id === id);

    emitComponent(foundComponent ? foundComponent : null);
  };
  const [components, setComponents] = useState<
    MechanicalComponentWithQuantitySearchResponse[]
  >([]);
  const [selectedProvider, setSelectedProvider] =
    useState<ProviderGetResponse | null>(null);
  const [totalItems, setTotalItems] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  const [searchText, setSearchText] = useState<string | null>(null);
  useEffect(() => {
    MechanicalComponentService.findFilteredForProductCreation({
      componentInfo: searchText,
      providerId: selectedProvider ? selectedProvider.id : null,
      pageNumber: pageNumber,
      pageSize: pageSize,
    }).then((response) => {
      console.log(response.data.items);
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
          onFetchPartners={fetchProviders}
        />
      </div>
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {components.map(
          (component: MechanicalComponentWithQuantitySearchResponse) => {
            return (
              <ComponentSearchSectionCard
                quantity={component.quantity}
                key={component.id}
                emitComponentId={handleComponentButtonClick}
                id={component.id}
                identifier={component.identifier}
                name={component.name}
              />
            );
          }
        )}
      </div>
    </div>
  );
};

export default ComponentSearchSection;
