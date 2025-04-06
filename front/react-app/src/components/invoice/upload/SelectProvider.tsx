import { useEffect, useState } from "react";
import { ProviderService } from "../../../services/ProviderService";
import { ProviderGetResponse } from "../../../model/provider/ProviderGetResponse";

interface SelectProviderProps {
  emitProvider: (provider: ProviderGetResponse | null) => void;
}
const SelectProvider = ({ emitProvider }: SelectProviderProps) => {
  const [providers, setProviders] = useState<ProviderGetResponse[]>([]);
  const [selectedProvider, setSelectedProvider] =
    useState<ProviderGetResponse | null>(null);
  useEffect(() => {
    ProviderService.FindAll()
      .then((value) => {
        setProviders(value.data.providers);
      })
      .catch((error) => {});
  }, []);
  useEffect(() => {
    emitProvider(selectedProvider);
  }, [selectedProvider]);
  const handleProviderSelect = (
    event: React.ChangeEvent<HTMLSelectElement>
  ) => {
    const selectedId = event.target.value;
    const foundProvider = providers.find((p) => p.id === selectedId);
    if (!foundProvider) {
      setSelectedProvider(null);
      return;
    }
    setSelectedProvider(foundProvider);
  };
  return (
    <form className="w-2/3 mx-auto">
      <label
        htmlFor="countries"
        className="block mb-2 text-lg font-medium text-white dark:text-white"
      >
        Select Provider
      </label>
      <select
        onChange={handleProviderSelect}
        id="countries"
        defaultValue={"Choose a provider"}
        className="bg-gray-50 border text-base border-gray-300 text-gray-900 rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
      >
        <option>Choose a provider</option>
        {providers.map((provider) => {
          return (
            <option key={provider.id} value={provider.id}>
              {provider.name}
            </option>
          );
        })}
      </select>
    </form>
  );
};

export default SelectProvider;
