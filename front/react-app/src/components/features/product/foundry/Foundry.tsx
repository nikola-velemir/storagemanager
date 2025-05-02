import { ChangeEvent, useState } from "react";
import FoundryStepper from "./stepper/FoundryStepper";
import ProductSearchSection from "./searchSections/ProductSearchSection";
import { ProductSearchResponse } from "../../../../model/product/bluePrint/ProductSearchResponse";
import ProductDisplayCard from "./cards/ProductDisplayCard";
import { toast } from "react-toastify";
import { ProductBatchService } from "../../../../services/products/ProductBatchService";
import { AxiosError } from "axios";

const Foundry = () => {
  const [currentStep, setCurrentStep] = useState(0);
  const [quantity, setQuantity] = useState(1);
  const [selectedProduct, setSelectedProduct] =
    useState<ProductSearchResponse | null>(null);

  const handleSelectedProductEmit = (p: ProductSearchResponse) => {
    setCurrentStep(1);
    setSelectedProduct(p);
  };
  const handleQuantityInput = (e: ChangeEvent<HTMLInputElement>) => {
    setQuantity(Number(e.target.value));
  };
  const onProduce = () => {
    if (quantity <= 0) {
      toast.error("Quantity must be 1 or higher!");
      return;
    }
    if (!selectedProduct) {
      toast.error("You must select a product!");
      return;
    }
    ProductBatchService.create({
      productId: selectedProduct.id,
      quantity: quantity,
    })
      .then(() => toast.success("Created succesfully"))
      .catch((error) => {
        console.log(error);
        if (error.response.data.name == "StockLimitExceeded") {
          toast.error(error.response.data.description);
          return;
        }
        toast.error("Failed to create batch");
      });
  };
  return (
    <div className="h-screen w-full p-8">
      <FoundryStepper step={currentStep} />
      {selectedProduct ? (
        <div className="w-full flex flex-col">
          <div className="w-full flex flex-row items-center justify-center">
            <div className="w-4/5">
              <ProductDisplayCard
                date={selectedProduct.dateCreated}
                id={selectedProduct.id}
                identifier={selectedProduct.identifier}
                name={selectedProduct.name}
              />
            </div>

            <div>
              <form className="max-w-sm mx-auto">
                <label
                  htmlFor="number-input"
                  className="block mb-2 text-lg font-medium text-white dark:text-white"
                >
                  Number of instances
                </label>
                <input
                  onChange={handleQuantityInput}
                  min={1}
                  type="number"
                  id="number-input"
                  aria-describedby="helper-text-explanation"
                  className="bg-gray-50 border border-gray-300 text-gray-900 text-sm rounded-lg focus:ring-blue-500 focus:border-blue-500 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                  placeholder="1"
                  required
                />
              </form>
            </div>
          </div>
          <button
            type="button"
            onClick={onProduce}
            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:ring-blue-300 font-medium rounded-lg text-lg px-5 py-2.5 me-2 mb-2 dark:bg-blue-600 dark:hover:bg-blue-700 focus:outline-none dark:focus:ring-blue-800"
          >
            Produce
          </button>
        </div>
      ) : (
        <ProductSearchSection emitSelectedProduct={handleSelectedProductEmit} />
      )}
    </div>
  );
};

export default Foundry;
