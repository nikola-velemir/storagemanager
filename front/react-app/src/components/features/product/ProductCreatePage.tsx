import { useCallback, useEffect, useState } from "react";
import ProductCreateForm, { ProductCreateFormData } from "./ProductCreateForm";
import ComponentSearchSection from "./sections/ComponentSearchSection";
import { MechanicalComponentSearchResponse } from "../../../model/components/search/MechanicalComponentSearchResponse";
import SelectedComponentsSection from "./sections/SelectedComponentsSection";

export interface ComponentWithQuantity {
  id: string;
  name: string;
  identifier: string;
  quantity: number;
}

const ProductCreatePage = () => {
  const [formData, setFormData] = useState<ProductCreateFormData>({
    productDescription: "",
    productName: "",
  });
  const [addedComponents, setAddedComponents] = useState<
    ComponentWithQuantity[]
  >([]);
  const handleProductFromDataChange = useCallback(
    (e: ProductCreateFormData) => {
      setFormData(e);
    },
    []
  );
  const handleAddComponentClick = (
    e: MechanicalComponentSearchResponse | null
  ) => {
    if (!e) return;
    const found = addedComponents.find((c) => c.id === e.id);
    if (found) return;

    setAddedComponents([...addedComponents, { ...e, quantity: 1 }]);
  };
  const handleRemoveComponentClick = (e: ComponentWithQuantity | null) => {
    if (!e) return;
    const found = addedComponents.find((c) => c.id === e.id);
    if (!found) return;
    setAddedComponents((prev) => prev.filter((c) => c.id !== found.id));
  };
  const handleComponentsUpdate = (c: ComponentWithQuantity[]) => {
    setAddedComponents(c);
  };
  const handleCreate = () => {
    console.log(addedComponents);
  };

  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 overflow-auto">
        <div className="w-full flex flex-col">
          <div className="w-full flex flex-row">
            <div className="px-2 py-8 w-1/3">
              <ProductCreateForm
                emitCreate={handleCreate}
                emitProductFormData={handleProductFromDataChange}
              />
            </div>
            <div className="px-2 py-8 w-2/3">
              <SelectedComponentsSection
                emitComponents={handleComponentsUpdate}
                emitComponent={handleRemoveComponentClick}
                components={addedComponents}
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
