import api from "../infrastructure/Interceptor/Interceptor";
import { MechanicalComponentPaginatedResponse } from "../model/components/MechanicalComponentPaginatedResponse";
import { MechanicalComponentSearchResponse } from "../model/components/MechanicalComponentSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";

export interface MechanicalComponentFilterRequest {
  pageSize: number;
  pageNumber: number;
}
export class MechanicalComponentService {
  public static async findFiltered(request: MechanicalComponentFilterRequest) {
    return api.get<PaginatedResponse<MechanicalComponentSearchResponse>>(
      "/components/filtered",
      {
        params: {
          pageSize: request.pageSize,
          pageNumber: request.pageNumber,
        },
      }
    );
  }
}
