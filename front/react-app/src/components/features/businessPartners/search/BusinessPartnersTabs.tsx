import { useState } from "react";
import ProviderSearch from "../provider/search/ProviderSearch";
import ExporterSearch from "../exporter/search/ExporterSearch";

enum TabState {
  PROVIDERS,
  EXPORTERS,
}

const BusinessPartnersTabs = () => {
  const [selectedTabState, setSelectedTabState] = useState(TabState.PROVIDERS);
  const handleProvidersTab = () => {
    if (selectedTabState !== TabState.PROVIDERS)
      setSelectedTabState(TabState.PROVIDERS);
  };
  const handleExportersTab = () => {
    if (selectedTabState !== TabState.EXPORTERS)
      setSelectedTabState(TabState.EXPORTERS);
  };
  const activeTab =
    "w-full inline-block p-4 text-gray-700 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500";
  const inactiveTab =
    "cursor-pointer w-full inline-block p-4 hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300";

  return (
    <div className="w-full flex flex-col">
      <ul className="flex flex-nowrap flex-row justify-center text-lg font-medium text-center text-gray-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400">
        <li className="w-full">
          <p
            onClick={handleProvidersTab}
            className={`${
              selectedTabState === TabState.PROVIDERS ? activeTab : inactiveTab
            }`}
          >
            Providers
          </p>
        </li>
        <li className="w-full">
          <p
            onClick={handleExportersTab}
            className={`${
              selectedTabState === TabState.EXPORTERS ? activeTab : inactiveTab
            }`}
          >
            Exporters
          </p>
        </li>
      </ul>
      <div className="w-full h-96 flex flex-col gap-4">
        {selectedTabState === TabState.PROVIDERS ? (
          <ProviderSearch />
        ) : (
          <ExporterSearch />
        )}
      </div>
    </div>
  );
};

export default BusinessPartnersTabs;
