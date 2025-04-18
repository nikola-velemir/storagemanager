import ExportProductSearchSection from "./sections/ExportProductSearchSection";
import ExporterSelectBox from "./ExporterSelectBox";
import ExportSelectedProductsSection from "./sections/ExportSelectedProductsSection";
import { useState } from "react";
import { toast } from "react-toastify";
import { FindExporterResponse } from "../../../../../model/exporter/FindExporterResponse";
import { ProductSearchResponse } from "../../../../../model/product/ProductSearchResponse";
import { ExportService } from "../../../../../services/invoice/ExportService";

export interface ProductSelectionTuple {
  id: string;
  name: string;
  identifier: string;
  quantity: number;
  price: number;
}

const ExportCreatePage = () => {
  const [addedProducts, setAddedProducts] = useState<ProductSelectionTuple[]>(
    []
  );
  const [selectedExporter, setSelectedExporter] =
    useState<null | FindExporterResponse>(null);
  const handleRemoveProduct = (tup: ProductSelectionTuple | null) => {
    if (!tup) return;
    const found = addedProducts.find((p) => p.id === tup.id);
    if (!found) return;
    setAddedProducts(addedProducts.filter((p) => p.id !== found.id));
  };
  const handleEmitProducts = (tup: ProductSelectionTuple[]) => {
    setAddedProducts(tup);
  };
  const handleAddProduct = (p: ProductSearchResponse | null) => {
    if (!p) return;
    const found = addedProducts.find((pr) => pr.id === p.id);
    if (found) return;
    setAddedProducts([...addedProducts, { ...p, price: 0.0, quantity: 0 }]);
  };
  const handleExporterChange = (item: FindExporterResponse | null) =>
    setSelectedExporter(item);

  const handleCreateClick = () => {
    if (!selectedExporter) {
      toast.error("You must select an exporter");
      return;
    }
    if (!addedProducts || addedProducts.length === 0) {
      toast.error("You must select atleast one product");
      return;
    }
    ExportService.create({
      products: addedProducts,
      providerId: selectedExporter.id,
    })
      .then(() => toast.success("Export created!"))
      .catch(() => toast.error("Something went wrong!"));
  };
  return (
    <div className="w-full flex flex-col items-center p-8">
      <div className="w-full flex flex-row justify-between">
        <ExporterSelectBox
          emitExporterChange={handleExporterChange}
        ></ExporterSelectBox>
        <button
          onClick={handleCreateClick}
          type="button"
          className="text-white bg-blue-700 hover:bg-blue-800 focus:outline-none focus:ring-4 focus:ring-blue-300 font-medium rounded-full text-sm px-5 py-2.5 text-center me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
        >
          Create
        </button>
      </div>
      <div className="w-full">
        <ExportSelectedProductsSection
          emitProducts={handleEmitProducts}
          products={addedProducts}
          emitProduct={handleRemoveProduct}
        />
      </div>
      <div className="w-full">
        <ExportProductSearchSection emitProduct={handleAddProduct} />
      </div>
    </div>
  );
};

export default ExportCreatePage;
