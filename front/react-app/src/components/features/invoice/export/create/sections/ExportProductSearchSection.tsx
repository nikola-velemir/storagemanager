import { useState, useEffect } from "react";
import ExportProductCard from "../cards/ExportProductCard";
import { ProductSearchResponse } from "../../../../../../model/product/bluePrint/ProductSearchResponse";
import { ProductService } from "../../../../../../services/products/ProductService";
import DatePickerComponent from "../../../../../common/inputs/DatePickerComponent";
import Paginator from "../../../../../common/inputs/Paginator";
import SearchBox from "../../../../../common/inputs/SearchBox";
export const convertDateToString = (date: Date | null) => {
  if (!date) {
    return null;
  }
  const year = date.getFullYear();
  const month = String(date.getMonth() + 1).padStart(2, "0");
  const day = String(date.getDate()).padStart(2, "0");
  return `${year}-${month}-${day}`;
};

interface ExportProductSearchSectionProps {
  emitProduct: (p: ProductSearchResponse | null) => void;
}

const ExportProductSearchSection = ({
  emitProduct,
}: ExportProductSearchSectionProps) => {
  const [products, setProducts] = useState<ProductSearchResponse[]>([]);
  const [totalItems, setTotalItems] = useState<number>(0);
  const [pageSize, setPageSize] = useState(5);
  const [pageNumber, setPageNumber] = useState(1);
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);
  const [productInfo, setProductInfo] = useState<string | null>(null);
  useEffect(() => {
    ProductService.findFiltered({
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
  const handleEmitProductId = (id: string) => {
    const found = products.find((p) => p.id === id);
    emitProduct(found ? found : null);
  };
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full pb-2 gap-4 flex flex-row justify-center items-end">
        <SearchBox
          placeholderText="Component info"
          onInput={handleInputChange}
        />
        <DatePickerComponent onDateChange={handleDateChange} />
        <Paginator
          totalItems={totalItems}
          onPageNumberChange={handlePageNumberChange}
          onPageSizeChange={handlePageSizeChange}
        />
      </div>
      <div className="h-5/6 overflow-y-auto flex items-center flex-col">
        {products.map((product: ProductSearchResponse) => {
          return (
            <ExportProductCard
              emitProductId={handleEmitProductId}
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

export default ExportProductSearchSection;
