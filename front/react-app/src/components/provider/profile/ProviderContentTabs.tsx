import { useState } from "react";
import { ProviderProfileInvoiceResponse } from "../../../model/provider/ProviderProfileInvoiceResponse";
import { ProviderProfileComponentResponse } from "../../../model/provider/ProviderProfileComponentResponse";
import ProviderModalInvoiceCard from "./cards/ProviderModalInvoiceCard";
import ProviderModalComponentCard from "./cards/ProviderModalComponentCard";

enum TabState {
  INVOICES,
  COMPONENTS,
}

interface ProviderContentTabsProps {
  invoices: ProviderProfileInvoiceResponse[];
  components: ProviderProfileComponentResponse[];
}

const ProviderContentTabs = ({
  invoices,
  components,
}: ProviderContentTabsProps) => {
  const [selectedTabState, setSelectedTabState] = useState(TabState.INVOICES);
  const handleInvoicesTab = () => {
    if (selectedTabState !== TabState.INVOICES)
      setSelectedTabState(TabState.INVOICES);
  };
  const handleComponentsTab = () => {
    if (selectedTabState !== TabState.COMPONENTS)
      setSelectedTabState(TabState.COMPONENTS);
  };
  const activeTab =
    "w-full inline-block p-4 text-gray-700 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500";
  const inactiveTab =
    "cursor-pointer w-full inline-block p-4 hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300";
  const renderInvoices = () => {
    return invoices.map((invoice: ProviderProfileInvoiceResponse) => (
      <ProviderModalInvoiceCard
        key={invoice.id}
        dateIssued={invoice.dateIssued}
        id={invoice.id}
      />
    ));
  };
  const renderComponents = () => {
    return components.map((component: ProviderProfileComponentResponse) => (
      <ProviderModalComponentCard
        key={component.identifier}
        id={component.id}
        identifier={component.identifier}
        name={component.name}
      />
    ));
  };
  return (
    <div className="w-full flex flex-col">
      <ul className="flex flex-nowrap flex-row justify-center text-lg font-medium text-center text-gray-500 border-b border-gray-200 dark:border-gray-700 dark:text-gray-400">
        <li className="w-full">
          <p
            onClick={handleInvoicesTab}
            className={`rounded-tl-xl ${
              selectedTabState === TabState.INVOICES ? activeTab : inactiveTab
            }`}
          >
            Invoices
          </p>
        </li>
        <li className="w-full">
          <p
            onClick={handleComponentsTab}
            className={`rounded-tr-xl ${
              selectedTabState === TabState.COMPONENTS ? activeTab : inactiveTab
            }`}
          >
            Components
          </p>
        </li>
      </ul>
      <div className="w-full h-96 flex flex-col gap-4">
        {selectedTabState === TabState.INVOICES
          ? renderInvoices()
          : renderComponents()}
      </div>
    </div>
  );
};

export default ProviderContentTabs;
