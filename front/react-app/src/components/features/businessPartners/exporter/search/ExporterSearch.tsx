import ExporterCard from "./card/ExporterCard";
import { ExporterService } from "../../../../../services/ExporterService";
import { ExporterSearchResponse } from "../../../../../model/exporter/ExporterSearchResponse";
import BusinessPartnetsSearchTab from "../../search/BusinessPartnetsSearchTab";

const ExporterSearch = () => {
  const handleFetchData = async (
    searchText: string | null,
    page: number,
    size: number
  ) => {
    const res = await ExporterService.findFiltered({
      exporterInfo: searchText,
      pageNumber: page,
      pageSize: size,
    });
    return { items: res.data.items, totalItems: res.data.totalCount };
  };
  const cardPropsMapper = (item: ExporterSearchResponse) => ({
    id: item.id,
    name: item.name,
    address: item.address,
    phoneNumber: item.phoneNumber,
    items: item.exports,
  });
  return (
    <BusinessPartnetsSearchTab
      searchPlaceHolder="Exporter info"
      CardComponent={ExporterCard}
      cardPropsMapper={cardPropsMapper}
      onFetchData={handleFetchData}
    />
  );
};

export default ExporterSearch;
