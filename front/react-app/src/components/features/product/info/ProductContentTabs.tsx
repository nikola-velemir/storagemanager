import React, { useState } from "react";
import { ProductInfoComponentResponse } from "../../../../model/product/ProductInfoComponentResponse";
import ProductInfoComponentCard from "./cards/ProductInfoComponentCard";
import { ProductInfoExportResponse } from "../../../../model/product/ProductInfoExportResponse";
import ProductInfoExportCard from "./cards/ProductInfoExportCard";

enum TabState {
  COMPONENTS,
  EXPORTS,
}

interface ProductContentTabsProps {
  components: ProductInfoComponentResponse[];
  exports: ProductInfoExportResponse[];
}
const ProductContentTabs = ({
  components,
  exports,
}: ProductContentTabsProps) => {
  const activeTab =
    "w-full inline-block p-4 text-gray-700 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500";
  const inactiveTab =
    "cursor-pointer w-full inline-block p-4 hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300";
  const [selectedTabState, setSelectedTabState] = useState(TabState.COMPONENTS);
  const handleComponentsTab = () => {
    setSelectedTabState(TabState.COMPONENTS);
  };
  const handleExportsTab = () => {
    setSelectedTabState(TabState.EXPORTS);
  };
  const renderExports = () => {
    return exports.map((exp: ProductInfoExportResponse) => (
      <ProductInfoExportCard
        date={exp.date}
        exporter={exp.exporter}
        id={exp.id}
        key={exp.id}
      />
    ));
  };
  const renderComponents = () => {
    return components.map((component: ProductInfoComponentResponse) => (
      <ProductInfoComponentCard
        id={component.id}
        identifier={component.identifier}
        name={component.name}
        quantity={component.quantity}
      />
    ));
  };
  return (
    <div className="w-full flex flex-col">
      <ul className="flex flex-nowrap flex-row justify-center text-lg font-medium text-center text-gray-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400">
        <li className="w-full">
          <p
            onClick={handleComponentsTab}
            className={`rounded-tl-xl ${
              selectedTabState === TabState.COMPONENTS ? activeTab : inactiveTab
            }`}
          >
            Components
          </p>
        </li>
        <li className="w-full">
          <p
            onClick={handleExportsTab}
            className={`rounded-tr-xl ${
              selectedTabState === TabState.EXPORTS ? activeTab : inactiveTab
            }`}
          >
            Exports
          </p>
        </li>
      </ul>
      <div className="w-full h-96 flex flex-col items-center gap-4">
        {selectedTabState === TabState.EXPORTS
          ? renderExports()
          : renderComponents()}
      </div>
    </div>
  );
};

export default ProductContentTabs;
