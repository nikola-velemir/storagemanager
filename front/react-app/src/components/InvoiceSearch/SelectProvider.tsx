import React, { useEffect, useState } from "react";
import { ProviderGetResponse } from "../../model/provider/ProviderGetResponse";

interface SelectProviderProps {
  providers: ProviderGetResponse[];
  emitProviderChange: (provider: ProviderGetResponse | null) => void;
}
const SelectProvider = ({
  providers,
  emitProviderChange,
}: SelectProviderProps) => {
  const [selectedProvider, setSelectedProvider] =
    useState<ProviderGetResponse | null>(null);
  useEffect(() => {
    emitProviderChange(selectedProvider);
  }, [selectedProvider]);
  const handleProviderChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const foundProvider = providers.find((p) => p.id === e.target.value.trim());
    setSelectedProvider(foundProvider ? foundProvider : null);
  };
  return (
    <form className="max-w-sm">
      <label
        htmlFor="providers"
        className="block mb-2 text-sm font-medium text-white dark:text-white"
      >
        Select provider
      </label>
      <select
        onChange={handleProviderChange}
        id="providers"
        className="h-10 bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
      >
        <option value={"none"}>Choose a country</option>
        {providers.map((provider: ProviderGetResponse) => {
          return <option value={provider.id}>{provider.name}</option>;
        })}
      </select>
    </form>
  );
};

export default SelectProvider;
