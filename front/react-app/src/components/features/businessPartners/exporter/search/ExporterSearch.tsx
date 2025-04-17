import { useState, useEffect } from "react";
import Paginator from "../../../../common/inputs/Paginator";
import SearchBox from "../../../../common/inputs/SearchBox";
import ExporterCard from "./card/ExporterCard";
import { ExporterService } from "../../../../../services/ExporterService";
import { ExporterSearchResponse } from "../../../../../model/exporter/ExporterSearchResponse";
import { toast } from "react-toastify";

const ExporterSearch = () => {
  const [exporters, setExporters] = useState<ExporterSearchResponse[]>([]);
  const [searchText, setSearchText] = useState<string | null>(null);
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(5);
  useEffect(() => {
    ExporterService.findFiltered({
      exporterInfo: searchText,
      pageNumber: pageNumber,
      pageSize: pageSize,
    })
      .then((res) => setExporters(res.data.items))
      .catch(() => toast.error("Failed to fetch exporters"));
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
        {exporters.map((provider: ExporterSearchResponse) => {
          return (
            <ExporterCard
              key={provider.id}
              exps={provider.exports}
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

export default ExporterSearch;
