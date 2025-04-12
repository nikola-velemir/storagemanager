import api from "../infrastructure/Interceptor/Interceptor";
import { ProductCreateRequest } from "../model/product/ProductCreateRequest";

export class ProductService {
  public static async createProduct(request: ProductCreateRequest) {
    return api.post("/products", request);
  }
}
