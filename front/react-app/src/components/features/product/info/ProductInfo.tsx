import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { ProductInfoResponse } from "../../../../model/product/ProductInfoResponse";
import { ProductService } from "../../../../services/ProductService";
import ProductContentTabs from "./ProductContentTabs";

const ProductInfo = () => {
  const { id } = useParams<{ id: string }>();
  const [product, setProduct] = useState<ProductInfoResponse | null>(null);
  useEffect(() => {
    if (!id) return;
    ProductService.findInfo(id).then((res) => {
      setProduct(res.data);
    });
  }, [id]);
  return (
    <div className="h-screen w-full p-8">
      <div className="w-full h-5/6 overflow-auto">
        <div className="px-8 pt-8 space-y-4">
          <div className="flex flex-row gap-12 w-full bg-gray-600 rounded-xl p-8">
            <div className="bg-slate-800 rounded-xl p-2">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                fill="none"
                viewBox="0 0 24 24"
                strokeWidth="1.5"
                stroke="currentColor"
                className="size-48"
              >
                <path
                  strokeLinecap="round"
                  strokeLinejoin="round"
                  d="m21 7.5-9-5.25L3 7.5m18 0-9 5.25m9-5.25v9l-9 5.25M3 7.5l9 5.25M3 7.5v9l9 5.25m0-9v9"
                />
              </svg>
            </div>
            <div className="flex flex-col justify-center w-full text-base text-gray-400">
              <div className="mt-2 flex flex-row items-center text-sm">
                Name:
                <span className="ms-3 text-white text-lg font-medium">
                  {product?.name}
                </span>
              </div>
              <div className="mt-2 flex flex-row items-center text-sm">
                Identifier:
                <span className="ms-3 text-white text-lg font-medium">
                  {product?.identifier}
                </span>
              </div>
              <div className="mt-2 flex flex-row items-center text-sm">
                Date created:
                <span className="ms-3 text-white text-lg font-medium">
                  {product?.dateCreated}
                </span>
              </div>
              <div className="mt-2 flex flex-row items-center text-sm">
                Description:
                <span className="ms-3 text-white text-lg font-medium">
                  {product?.description}
                </span>
              </div>
            </div>
          </div>
          <ProductContentTabs components={product ? product.components : []} />
        </div>
      </div>
    </div>
  );
};

export default ProductInfo;
