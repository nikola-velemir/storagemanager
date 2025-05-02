import api from "../../infrastructure/Interceptor/Interceptor";
import { ProductBatchCreateRequest } from "../../model/product/batch/ProductBatchCreateRequest";

export class ProductBatchService {
  private static baseURL = "/product-batches";

  public static async create(request: ProductBatchCreateRequest) {
    return api.post(this.baseURL, request);
  }
}
