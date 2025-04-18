import { ProviderSearchResponse } from "../../../../../model/provider/ProviderSearchResponse";
import BusinessPartnetsSearchTab from "../../search/containers/BusinessPartnetsSearchTab";
import BusinessPartnerCard from "../../search/cards/BusinessPartnerCard";
import { useNavigate } from "react-router-dom";
import { ProviderService } from "../../../../../services/businessPartner/ProviderService";

const ProviderSearch = () => {
  const navigate = useNavigate();
  const handleFetchData = async (
    searchText: string | null,
    page: number,
    size: number
  ) => {
    const res = await ProviderService.findFiltered({
      providerName: searchText,
      pageNumber: page,
      pageSize: size,
    });
    return { items: res.data.items, totalItems: res.data.totalCount };
  };
  const cardPropsMapper = (item: ProviderSearchResponse) => ({
    id: item.id,
    name: item.name,
    address: item.address,
    phoneNumber: item.phoneNumber,
    items: item.invoices,
    handleMoreInfoClick: () => navigate("/exporter-info/" + item.id),
  });
  return (
    <BusinessPartnetsSearchTab
      searchPlaceHolder="Provider info"
      CardComponent={BusinessPartnerCard}
      cardPropsMapper={cardPropsMapper}
      onFetchData={handleFetchData}
    />
  );
};

export default ProviderSearch;
