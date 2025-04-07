import { useEffect, useState } from "react";
import { MechanicalComponentService } from "../../../services/MechanicalComponentService";
import { MechanicalComponentSearchResponse } from "../../../model/components/MechanicalComponentSearchResponse";
import ComponentCard from "./cards/ComponentCard";
import InvoiceSearchPagination from "../inputs/Paginator";

const ComponentsSearch = () => {
  const [components, setComponents] = useState<
    MechanicalComponentSearchResponse[]
  >([]);
  const [totalItems, setTotalItems] = useState(0);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  useEffect(() => {
    MechanicalComponentService.findFiltered({
      pageNumber: pageNumber,
      pageSize: pageSize,
    }).then((response) => {
      setComponents(response.data.items);
      setTotalItems(response.data.totalCount);
    });
  }, [pageNumber, pageSize]);
  const handlePageSizeChange = (n: number) => {
    setPageSize(n);
  };
  const handlePageNumberChange = (n: number) => {
    setPageNumber(n);
  };
  return (
    <div className="h-screen w-full p-8">
      <InvoiceSearchPagination
        totalItems={totalItems}
        onPageSizeChange={handlePageSizeChange}
        onPageNumberChange={handlePageNumberChange}
      />
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {components.map((component: MechanicalComponentSearchResponse) => {
          return (
            <ComponentCard
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
