import { useCallback, useEffect, useState } from "react";
import ProductCreateForm, { ProductCreateFormData } from "./ProductCreateForm";
import ComponentSearchSection from "./ComponentSearchSection";
import { MechanicalComponentSearchResponse } from "../../../model/components/search/MechanicalComponentSearchResponse";

const ProductCreatePage = () => {
  const [formData, setFormData] = useState<ProductCreateFormData>({
    productDescription: "",
    productName: "",
  });
  const [addedComponent, setAddedComponent] =
    useState<null | MechanicalComponentSearchResponse>(null);
  const handleProductFromDataChange = useCallback(
    (e: ProductCreateFormData) => {
      setFormData(e);
    },
    []
  );
  const handleAddComponentClick = (
    e: MechanicalComponentSearchResponse | null
  ) => {
    setAddedComponent(e);
  };

  useEffect(() => {
    console.log(addedComponent);
  }, [addedComponent]);

  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 overflow-auto">
        <div className="w-full flex flex-col">
          <div className="w-full flex flex-row">
            <div className="px-2 py-8 w-1/3">
              <ProductCreateForm
                emitProductFormData={handleProductFromDataChange}
              />
            </div>
          </div>
          <div className="w-full flex flex-row">
            <ComponentSearchSection emitComponent={handleAddComponentClick} />
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductCreatePage;
