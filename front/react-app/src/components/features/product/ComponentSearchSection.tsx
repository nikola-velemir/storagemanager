import { useEffect, useState } from "react";
import { MechanicalComponentSearchResponse } from "../../../model/components/search/MechanicalComponentSearchResponse";
import { ProviderGetResponse } from "../../../model/provider/ProviderGetResponse";
import { MechanicalComponentService } from "../../../services/MechanicalComponentService";
import { ProviderService } from "../../../services/ProviderService";
import Paginator from "../../common/inputs/Paginator";
import SearchBox from "../../common/inputs/SearchBox";
import SelectProviderBox from "../invoice/search/cards/SelectProviderBox";
import ComponentSearchSectionCard from "./ComponentSearchSectionCard";

interface ComponentSearchSectionProps {
  emitComponent: (component: MechanicalComponentSearchResponse | null) => void;
}

const ComponentSearchSection = ({
  emitComponent,
}: ComponentSearchSectionProps) => {
  const handleComponentButtonClick = (id: string) => {
    const foundComponent = components.find((i) => i.id === id);

    emitComponent(foundComponent ? foundComponent : null);
  };
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
    ProviderService.findAll().then((response) => {
      setProviders(response.data.providers);
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
        <SelectProviderBox
          emitProviderChange={handleProviderChange}
          providers={providers}
        />
      </div>
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {components.map((component: MechanicalComponentSearchResponse) => {
          return (
            <ComponentSearchSectionCard
              emitComponentId={handleComponentButtonClick}
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

export default ComponentSearchSection;
