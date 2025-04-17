import { useCallback, useEffect, useState } from "react";
import { ExportSearchResponse } from "../../../../../model/invoice/export/ExportSearchResponse";
import { ExportService } from "../../../../../services/ExportService";
import ExporterSelectBox from "../create/ExporterSelectBox";
import DatePickerComponent from "../../../../common/inputs/DatePickerComponent";
import Paginator from "../../../../common/inputs/Paginator";
import SearchBox from "../../../../common/inputs/SearchBox";
import { FindExporterResponse } from "../../../../../model/exporter/FindExporterResponse";
import ExportSearchCard from "./cards/ExportSearchCard";

const ExportSearch = () => {
  const [exports, setExports] = useState<ExportSearchResponse[]>([]);
  const [totalItems, setTotalItems] = useState(0);
  const [pageSize, setPageSize] = useState(5);
  const [pageNumber, setPageNumber] = useState(1);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [productInfo, setProductInfo] = useState<string | null>(null);
  const [selectedExporter, setSelectedExporter] =
    useState<FindExporterResponse | null>(null);
  const fetchExports = useCallback(() => {
    ExportService.findFiltered({
      pageNumber: pageNumber,
      pageSize: pageSize,
    }).then((response) => {
      setExports(response.data.items);
      setTotalItems(response.data.totalCount);
    });
  }, [productInfo, pageSize, pageNumber, selectedDate, selectedExporter]);
  useEffect(() => {
    fetchExports();
  }, [fetchExports]);
  const handleExporterChange = (item: FindExporterResponse | null) =>
    setSelectedExporter(item);
  const handleInputChange = (text: string) =>
    setProductInfo(text.trim().length > 0 ? text.trim() : null);
  const handleDateChange = (e: Date | null) => {
    setSelectedDate(e);
    setPageNumber(1);
  };
  const handlePageSizeChange = (newPageSize: number) => {
    setPageSize(newPageSize);
    setPageNumber(1);
  };
  const handlePageNumberChange = (newPageNumber: number) => {
    setPageNumber(newPageNumber);
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full pb-2 gap-4 flex flex-row justify-center items-end">
        <SearchBox onInput={handleInputChange} placeholderText="Product info" />
        <DatePickerComponent onDateChange={handleDateChange} />
        <Paginator
          totalItems={totalItems}
          onPageNumberChange={handlePageNumberChange}
          onPageSizeChange={handlePageSizeChange}
        />
        <ExporterSelectBox emitExporterChange={handleExporterChange} />
      </div>

      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {exports.map((e: ExportSearchResponse) => {
          return (
            <ExportSearchCard
              products={e.products}
              exporterName={e.exporterName}
              date={e.date}
              id={e.id}
              key={e.id}
            />
          );
        })}
      </div>
    </div>
  );
};

export default ExportSearch;
