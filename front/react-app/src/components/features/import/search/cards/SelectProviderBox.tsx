import React from "react";
import SelectBox from "../../../../common/inputs/SelectBox";
import { ProviderGetResponse } from "../../../../../model/provider/ProviderGetResponse";

interface SelectProviderBoxProps {
  providers: ProviderGetResponse[];
  emitProviderChange: (provider: ProviderGetResponse | null) => void;
}
const SelectProviderBox = ({
  providers,
  emitProviderChange,
}: SelectProviderBoxProps) => {
  const getProviderId = (provider: ProviderGetResponse) => provider.id;
  const getProviderName = (provider: ProviderGetResponse) => provider.name;
  return (
    <SelectBox
      items={providers}
      displayText="Select providers"
      emitSelectionChange={emitProviderChange}
      getItemId={getProviderId}
      getItemName={getProviderName}
    />
  );
};

export default SelectProviderBox;
