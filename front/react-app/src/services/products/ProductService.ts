import api from "../../infrastructure/Interceptor/Interceptor";
import { ExportSearchProductResponse } from "../../model/invoice/export/ExportSearchProductResponse";
import { PaginatedResponse } from "../../model/PaginatedResponse";
import { ProductCreateRequest } from "../../model/product/bluePrint/ProductCreateRequest";
import { ProductFilterRequest } from "../../model/product/bluePrint/ProductFilterRequest";
import { ProductFindResponse } from "../../model/product/bluePrint/ProductFindResponse";
import { ProductSearchResponse } from "../../model/product/bluePrint/ProductSearchResponse";
import { ProductSearchWithQuantityResponse } from "../../model/product/bluePrint/ProductSearchWithQuantity";

export class ProductService {
  private static BASE_URL = "/products";
  public static async findByInvoiceId(id: string) {
    return api.get<{ products: ExportSearchProductResponse[] }>(
      this.BASE_URL + "/find-by-invoice/" + id
    );
  }
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
  public static async findFilteredWithQuantity(filter: ProductFilterRequest) {
    return api.get<PaginatedResponse<ProductSearchWithQuantityResponse>>(
      this.BASE_URL + "/filtered-with-quantity",
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
  public static async findFilteredWithMaxQuantity(
    filter: ProductFilterRequest
  ) {
    return api.get<PaginatedResponse<ProductSearchWithQuantityResponse>>(
      this.BASE_URL + "/filtered-with-max-quantity",
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
  public static async findByPartner(id: string) {
    return api.get<ProductFindResponse[]>(this.BASE_URL + "/partner/" + id);
  }
  public static async findInfo(id: string) {
    return api.get(this.BASE_URL + "/info/" + id);
  }
}
