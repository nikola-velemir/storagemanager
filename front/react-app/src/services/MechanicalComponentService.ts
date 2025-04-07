import api from "../infrastructure/Interceptor/Interceptor";
import { MechanicalComponentSearchResponse } from "../model/components/MechanicalComponentSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";

export interface MechanicalComponentFilterRequest {
  pageSize: number;
  pageNumber: number;
  providerId: string | null;
  componentInfo: string | null;
}
export class MechanicalComponentService {
  public static async findFiltered(request: MechanicalComponentFilterRequest) {
    return api.get<PaginatedResponse<MechanicalComponentSearchResponse>>(
      "/components/filtered",
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
