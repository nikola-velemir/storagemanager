import { ImportSearchResponse } from "../../../../../model/invoice/import/ImportSearchResponse";
import { ImportService } from "../../../../../services/ImportService";
import { ProviderService } from "../../../../../services/ProviderService";
import InvoiceSearchTab from "../../search/InvoiceSearchTab";
import InvoiceSearchCard from "../../search/cards/InvoiceSearchCard";
import { useNavigate } from "react-router-dom";

export const convertDateToString = (date: Date | null) => {
  if (!date) {
    return null;
  }
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};

const ImportSearch = () => {
  const handleFetchData = async (
    page: number,
    size: number,
    searchText: string | null,
    date: Date | null,
    partnerId: string | null
  ) => {
    const res = await ImportService.findFiltered({
      componentInfo: searchText,
      date: convertDateToString(date),
      id: partnerId,
      pageNumber: page,
      pageSize: size,
    });
    return { data: res.data.items, totalCount: res.data.totalCount };
  };
  const fetchPartners = async () => {
    const res = await ProviderService.findAll();
    return { data: res.data.providers };
  };

  const navigate = useNavigate();
  const handleComponentPageSwitch = (id: string) =>
    navigate("/component-info/" + id);

  const cardPropsMapper = (item: ImportSearchResponse) => ({
    id: item.id,
    date: item.date,
    partnerName: item.provider.name,
    items: item.components,
    handleRouting: handleComponentPageSwitch,
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

export default ImportSearch;
