import React, { useState } from "react";
import ImportSearch from "./import/search/ImportSearch";

enum TabState {
  IMPORTS,
  EXPORTS,
}

const InvoiceSearch = () => {
  const [selectedTabState, setSelectedTabState] = useState(TabState.IMPORTS);
  const handleImportsTab = () => {
    setSelectedTabState(TabState.IMPORTS);
  };
  const handleExportsTab = () => {
    setSelectedTabState(TabState.EXPORTS);
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
            onClick={handleImportsTab}
            className={`${
              selectedTabState === TabState.IMPORTS ? activeTab : inactiveTab
            }`}
          >
            Imports
          </p>
        </li>
        <li className="w-full">
          <p
            onClick={handleExportsTab}
            className={`${
              selectedTabState === TabState.EXPORTS ? activeTab : inactiveTab
            }`}
          >
            Components
          </p>
        </li>
      </ul>
      <div
        className={`${
          selectedTabState === TabState.IMPORTS ? "" : "hidden"
        } w-full h-96 flex flex-col gap-4`}
      >
        <ImportSearch />
      </div>
      <div
        className={`${
          selectedTabState === TabState.EXPORTS ? "" : "hidden"
        } w-full h-96 flex flex-col gap-4`}
      >
        <ImportSearch />
      </div>
    </div>
  );
};

export default InvoiceSearch;
