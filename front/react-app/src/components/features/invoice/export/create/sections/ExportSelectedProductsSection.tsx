import { useEffect, useState } from "react";
import ExportSelectedProductCard, {
  SelectedProductTuple,
} from "../cards/ExportSelectedProductCard";
import { ProductSelectionTuple } from "../ExportCreatePage";

interface ExportSelectedProductsSectionProps {
  products: ProductSelectionTuple[];
  emitProduct: (c: ProductSelectionTuple | null) => void;
  emitProducts: (c: ProductSelectionTuple[]) => void;
}
const ExportSelectedProductsSection = ({
  products,
  emitProduct,
  emitProducts,
}: ExportSelectedProductsSectionProps) => {
  const [selectedProducts, setSelectedProducts] = useState<
    ProductSelectionTuple[]
  >([]);
  useEffect(() => setSelectedProducts(products), [products]);
  const handleRemoveClick = (id: string) => {
    const found = products.find((p) => p.id === id);
    emitProduct(found ? found : null);
  };
  const handleEmitTuple = (tup: SelectedProductTuple) => {
    setSelectedProducts((prev) => {
      const updatedProducts = prev.map((product: ProductSelectionTuple) =>
        product.id === tup.id
          ? {
              ...product,
              quantity: tup.quantity,
              price: tup.pricePerPiece,
            }
          : product
      );
      emitProducts(updatedProducts);
      return updatedProducts;
    });
  };
  return (
    <div className="w-full h-96 flex flex-col items-center overflow-y-scroll">
      {products.map((product: ProductSelectionTuple) => (
        <ExportSelectedProductCard
          maxQuantity={product.maxQuantity}
          key={product.id}
          emitProductForValidation={handleEmitTuple}
          emitProductId={handleRemoveClick}
          id={product.id}
          identifier={product.identifier}
          name={product.name}
        />
      ))}
    </div>
  );
};

export default ExportSelectedProductsSection;
