import { ExporterSearchResponse } from "../../../../../model/exporter/ExporterSearchResponse";
import BusinessPartnetsSearchTab from "../../search/containers/BusinessPartnetsSearchTab";
import BusinessPartnerCard from "../../search/cards/BusinessPartnerCard";
import { useNavigate } from "react-router-dom";
import { ExporterService } from "../../../../../services/businessPartner/ExporterService";

const ExporterSearch = () => {
  const navigate = useNavigate();
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
    handleMoreInfoClick: () => navigate("/exporter-info/" + item.id),
  });
  return (
    <BusinessPartnetsSearchTab
      searchPlaceHolder="Exporter info"
      CardComponent={BusinessPartnerCard}
      cardPropsMapper={cardPropsMapper}
      onFetchData={handleFetchData}
    />
  );
};

export default ExporterSearch;
