import SelectBox from "../../../common/inputs/SelectBox";
import { ProviderGetResponse } from "../../../../model/provider/ProviderGetResponse";
import { useEffect, useState } from "react";

export interface PartnerLike {
  id: string;
  name: string;
}

interface SelectBusinessPartnerBoxProps<T> {
  onFetchPartners: () => Promise<{ data: T[] }>;
  emitPartnerChange: (partner: T | null) => void;
}
const SelectBusinessPartnerBox = <T extends PartnerLike>({
  emitPartnerChange,
  onFetchPartners,
}: SelectBusinessPartnerBoxProps<T>) => {
  const [partners, setPartners] = useState<T[]>([]);
  const getProviderId = (provider: T) => provider.id;
  const getProviderName = (provider: T) => provider.name;

  useEffect(() => {
    onFetchPartners().then((res) => setPartners(res.data));
  }, []);
  return (
    <SelectBox
      items={partners}
      displayText="Select providers"
      emitSelectionChange={emitPartnerChange}
      getItemId={getProviderId}
      getItemName={getProviderName}
    />
  );
};

export default SelectBusinessPartnerBox;
