import { stat } from "fs";
import api from "../infrastructure/Interceptor/Interceptor";
import { MechanicalComponentFindResponses } from "../model/components/find/MechanicalComponentFindResponses";
import { MechanicalComponentInfoResponse } from "../model/components/info/MechanicalComponentInfoResponse";
import { MechanicalComponentQuantitySumResponse } from "../model/components/MechanicalComponentQuantitySumResponse";
import { MechanicalComponentSearchResponse } from "../model/components/search/MechanicalComponentSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";
import { MechanicalComponentTopFiveQuantityResponses } from "../model/components/MechanicalComponentTopFiveQuantityResponses";

export interface MechanicalComponentFilterRequest {
  pageSize: number;
  pageNumber: number;
  providerId: string | null;
  componentInfo: string | null;
}
export class MechanicalComponentService {
  static findFilteredForProduct(request: MechanicalComponentFilterRequest) {
    return api.get<PaginatedResponse<MechanicalComponentSearchResponse>>(
      this.BASE_URL + "/filtered-product",
      {
        params: {
          componentInfo: request.componentInfo,
          providerId: request.providerId,
          pageSize: request.pageSize,
          pageNumber: request.pageNumber,
        },
      }
    );
  }
  static findTopFiveInQuantity() {
    return api.get<MechanicalComponentTopFiveQuantityResponses>(
      this.BASE_URL + "/find-top-five-quantity"
    );
  }
  static findComponentCount() {
    return api.get<MechanicalComponentQuantitySumResponse>(
      this.BASE_URL + "/find-quantity"
    );
  }
  private static BASE_URL = "/components";
  public static findInfo(id: string) {
    return api.get<MechanicalComponentInfoResponse>(
      this.BASE_URL + "/info/" + id
    );
  }
  public static async findByInvoiceId(invoiceId: string) {
    return api.get<MechanicalComponentFindResponses>(
      this.BASE_URL + "/find-by-invoice/" + invoiceId
    );
  }
  public static async findFiltered(request: MechanicalComponentFilterRequest) {
    return api.get<PaginatedResponse<MechanicalComponentSearchResponse>>(
      this.BASE_URL + "/filtered",
      {
        params: {
          componentInfo: request.componentInfo,
          providerId: request.providerId,
          pageSize: request.pageSize,
          pageNumber: request.pageNumber,
        },
      }
    );
  }
}
