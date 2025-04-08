import api from "../infrastructure/Interceptor/Interceptor";
import { MechanicalComponentFindResponses } from "../model/components/find/MechanicalComponentFindResponses";
import { MechanicalComponentInfoResponse } from "../model/components/info/MechanicalComponentInfoResponse";
import { MechanicalComponentSearchResponse } from "../model/components/search/MechanicalComponentSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";

export interface MechanicalComponentFilterRequest {
  pageSize: number;
  pageNumber: number;
  providerId: string | null;
  componentInfo: string | null;
}
export class MechanicalComponentService {
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
