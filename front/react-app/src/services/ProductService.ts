import api from "../infrastructure/Interceptor/Interceptor";
import { PaginatedResponse } from "../model/PaginatedResponse";
import { ProductCreateRequest } from "../model/product/ProductCreateRequest";
import { ProductFilterRequest } from "../model/product/ProductFilterRequest";
import { ProductInfoResponse } from "../model/product/ProductInfoResponse";
import { ProductSearchResponse } from "../model/product/ProductSearchResponse";

export class ProductService {
  private static BASE_URL = "/products";
  public static async createProduct(request: ProductCreateRequest) {
    return api.post(this.BASE_URL, request);
  }
  public static async findFiltered(filter: ProductFilterRequest) {
    return api.get<PaginatedResponse<ProductSearchResponse>>(
      this.BASE_URL + "/filtered",
      {
        params: {
          pageNumber: filter.pageNumber,
          pageSize: filter.pageSize,
          dateCreated: filter.dateCreated,
          productInfo: filter.productInfo,
        },
      }
    );
  }
  public static async findInfo(id: string) {
    return api.get<ProductInfoResponse>(this.BASE_URL + "/info/" + id);
  }
}
