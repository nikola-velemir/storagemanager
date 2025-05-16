import { useEffect, useState } from "react";
import { toast } from "react-toastify";
import { InvoiceFindResponse } from "../../../../../model/invoice/InvoiceFindResponse";
import { InvoiceService } from "../../../../../services/invoice/InvoiceService";
import BusinessPartnerProfileInvoiceCard from "../../profile/cards/BusinessPartnerProfileInvoiceCard";
import { ProductService } from "../../../../../services/products/ProductService";
import { ProductFindResponse } from "../../../../../model/product/bluePrint/ProductFindResponse";
import ExporterProfileProductCard from "./cards/ExporterProfileProductCard";

enum TabState {
  EXPORTS,
  PRODUCTS,
}
interface ExporterContentTabsProps {
  id: string;
}

const ExporterContentTabs = ({ id }: ExporterContentTabsProps) => {
  const [invoices, setInvoices] = useState<InvoiceFindResponse[]>([]);
  const [products, setProducts] = useState<ProductFindResponse[]>([]);
  const [selectedTabState, setSelectedTabState] = useState(TabState.EXPORTS);
  const handleInvoicesTab = () => {
    if (selectedTabState !== TabState.EXPORTS)
      setSelectedTabState(TabState.EXPORTS);
  };
  const handleProductsTab = () => {
    if (selectedTabState !== TabState.PRODUCTS)
      setSelectedTabState(TabState.PRODUCTS);
  };
  useEffect(() => {
    InvoiceService.findInvoiceByPartner(id)
      .then((res) => setInvoices(res.data))
      .catch(() => toast.error("Could not fetch invoices!"));
    ProductService.findByPartner(id)
      .then((res) => setProducts(res.data))
      .catch(() => toast.error("Could not fetch components!"));
  }, [id]);
  const activeTab =
    "w-full inline-block p-4 text-gray-700 bg-gray-100 active dark:bg-gray-800 dark:text-blue-500";
  const inactiveTab =
    "cursor-pointer w-full inline-block p-4 hover:text-gray-600 hover:bg-gray-50 dark:hover:bg-gray-800 dark:hover:text-gray-300";
  const renderInvoices = () => {
    return invoices.map((invoice: InvoiceFindResponse) => (
      <BusinessPartnerProfileInvoiceCard
        dateIssued={invoice.dateIssued}
        id={invoice.id}
        key={invoice.id}
      />
    ));
  };
  const renderProducts = () => {
    return products.map((product: ProductFindResponse) => (
      <ExporterProfileProductCard
        key={product.identifier}
        id={product.id}
        identifier={product.identifier}
        name={product.name}
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
              selectedTabState === TabState.EXPORTS ? activeTab : inactiveTab
            }`}
          >
            Exports
          </p>
        </li>
        <li className="w-full">
          <p
            onClick={handleProductsTab}
            className={`rounded-tr-xl ${
              selectedTabState === TabState.PRODUCTS ? activeTab : inactiveTab
            }`}
          >
            Products
          </p>
        </li>
      </ul>
      <div className="w-full h-96 flex flex-col gap-4">
        {selectedTabState === TabState.EXPORTS
          ? renderInvoices()
          : renderProducts()}
      </div>
    </div>
  );
};

export default ExporterContentTabs;
