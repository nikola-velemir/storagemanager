import { useCallback, useEffect, useState } from "react";
import { ExportSearchResponse } from "../../../../../model/invoice/export/ExportSearchResponse";
import { ExportService } from "../../../../../services/ExportService";
import ExporterSelectBox from "../create/ExporterSelectBox";
import DatePickerComponent from "../../../../common/inputs/DatePickerComponent";
import Paginator from "../../../../common/inputs/Paginator";
import SearchBox from "../../../../common/inputs/SearchBox";
import { FindExporterResponse } from "../../../../../model/exporter/FindExporterResponse";
import ExportSearchCard from "./cards/ExportSearchCard";
import { useNavigate } from "react-router-dom";
import { ExporterService } from "../../../../../services/ExporterService";
import { ImportService } from "../../../../../services/ImportService";
import InvoiceSearchCard from "../../search/cards/InvoiceSearchCard";
import InvoiceSearchTab from "../../search/InvoiceSearchTab";

export const convertDateToString = (date: Date | null) => {
  if (!date) {
    return null;
  }
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};
const ExportSearch = () => {
  const handleFetchData = async (
    page: number,
    size: number,
    searchText: string | null,
    date: Date | null,
    partnerId: string | null
  ) => {
    const res = await ExportService.findFiltered({
      productInfo: searchText,
      exporterId: partnerId,
      date: convertDateToString(date),
      pageNumber: page,
      pageSize: size,
    });
    return { data: res.data.items, totalCount: res.data.totalCount };
  };
  const fetchPartners = async () => {
    const res = await ExporterService.findAll();
    return { data: res.data.responses };
  };

  const navigate = useNavigate();
  const handleProductPageSwitch = (id: string) =>
    navigate("/product-info/" + id);

  const cardPropsMapper = (item: ExportSearchResponse) => ({
    id: item.id,
    date: item.date,
    partnerName: item.exporterName,
    items: item.products,
    handleRouting: handleProductPageSwitch,
  });
  return (
    <InvoiceSearchTab
      CardComponent={InvoiceSearchCard}
      fetchPartners={fetchPartners}
      cardPropsMapper={cardPropsMapper}
      fetchItems={handleFetchData}
    />
  );
};

export default ExportSearch;
