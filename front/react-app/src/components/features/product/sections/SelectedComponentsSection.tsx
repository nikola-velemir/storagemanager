import { useEffect, useState } from "react";
import SelectedComponentCard, {
  ComponentQuantityPair,
} from "../cards/SelectedComponentCard";
import { MechanicalComponentSearchResponse } from "../../../../model/components/search/MechanicalComponentSearchResponse";
import { ComponentWithQuantity } from "../ProductCreatePage";

interface SelectedComponentsSectionProps {
  components: ComponentWithQuantity[];
  emitComponent: (c: ComponentWithQuantity | null) => void;
  emitComponents: (c: ComponentWithQuantity[]) => void;
}

const SelectedComponentsSection = ({
  components,
  emitComponent,
  emitComponents,
}: SelectedComponentsSectionProps) => {
  const [selectedComponents, setSelectedComponents] = useState<
    ComponentWithQuantity[]
  >([]);
  useEffect(() => {
    setSelectedComponents(components);
  }, [components]);
  const handleRemoveClick = (id: string) => {
    const found = selectedComponents.find((c) => c.id === id);
    emitComponent(found ? found : null);
  };
  const handleComponentPairEmit = (pair: ComponentQuantityPair) => {
    setSelectedComponents((prev) => {
      const updatedComponents = prev.map((component: ComponentWithQuantity) =>
        component.id === pair.id
          ? { ...component, quantity: pair.quantity }
          : component
      );
      emitComponents(updatedComponents);
      return updatedComponents;
    });
  };
  return (
    <div className="w-full h-96 flex flex-col items-center overflow-y-scroll">
      {selectedComponents.map((component) => {
        return (
          <SelectedComponentCard
            emitComponentForValidation={handleComponentPairEmit}
            key={component.id}
            id={component.id}
            identifier={component.identifier}
            name={component.name}
            emitComponentId={handleRemoveClick}
          />
        );
      })}
    </div>
  );
};

export default SelectedComponentsSection;
