import { useEffect, useState } from "react";
import DatePickerComponent from "../../../../common/inputs/DatePickerComponent";
import Paginator from "../../../../common/inputs/Paginator";
import SearchBox from "../../../../common/inputs/SearchBox";
import { ProductService } from "../../../../../services/products/ProductService";
import { convertDateToString } from "../../search/ProductSearch";
import ProductSearchSectionCard from "../cards/ProductSearchSectionCard";
import { ProductSearchWithQuantityResponse } from "../../../../../model/product/bluePrint/ProductSearchWithQuantity";

interface ProductSearchSectionProps {
  emitSelectedProduct: (p: ProductSearchWithQuantityResponse) => void;
}
const ProductSearchSection = ({
  emitSelectedProduct,
}: ProductSearchSectionProps) => {
  const [products, setProducts] = useState<ProductSearchWithQuantityResponse[]>(
    []
  );
  const [totalItems, setTotalItems] = useState<number>(0);
  const [pageSize, setPageSize] = useState(5);
  const [pageNumber, setPageNumber] = useState(1);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [productInfo, setProductInfo] = useState<string | null>(null);
  useEffect(() => {
    ProductService.findFilteredWithMaxQuantity({
      dateCreated: convertDateToString(selectedDate),
      pageNumber: pageNumber,
      pageSize: pageSize,
      productInfo: productInfo,
    }).then((response) => {
      setProducts(response.data.items);
      setTotalItems(response.data.totalCount);
    });
  }, [pageSize, pageNumber, selectedDate, productInfo]);
  const handleDateChange = (e: Date | null) => {
    setSelectedDate(e);
    setPageNumber(1);
  };
  const handleInputChange = (text: string) => {
    setProductInfo(text.trim().length > 0 ? text.trim() : null);
  };
  const handlePageSizeChange = (newPageSize: number) => {
    setPageSize(newPageSize);
    setPageNumber(1);
  };
  const handlePageNumberChange = (newPageNumber: number) => {
    setPageNumber(newPageNumber);
  };
  const handleEmitProduct = (id: string) => {
    const found = products.find((p) => p.id === id);
    if (!found) return null;
    emitSelectedProduct(found);
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full pb-2 gap-4 flex flex-row justify-center items-end">
        <SearchBox placeholderText="Product info" onInput={handleInputChange} />
        <DatePickerComponent onDateChange={handleDateChange} />
        <Paginator
          totalItems={totalItems}
          onPageNumberChange={handlePageNumberChange}
          onPageSizeChange={handlePageSizeChange}
        />
      </div>
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {products.map((product: ProductSearchWithQuantityResponse) => {
          return (
            <ProductSearchSectionCard
              emitProductId={handleEmitProduct}
              id={product.id}
              key={product.id}
              date={product.dateCreated}
              identifier={product.identifier}
              name={product.name}
            />
          );
        })}
      </div>
    </div>
  );
};

export default ProductSearchSection;
