import api from "../infrastructure/Interceptor/Interceptor";
import { ImportCountThisWeekResponse } from "../model/invoice/import/ImportFindCountsForWeekResponseDTO";
import { ImportSearchResponse } from "../model/invoice/import/ImportSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";

export interface ImportFilterRequest {
  componentInfo: string | null;
  id: string | null;
  date: string | null;
  pageNumber: number;
  pageSize: number;
}

export interface InventoryValueByDay {
  value: number;
  day: string;
}

export interface TotalInventoryValueRequest {
  total: number;
  values: InventoryValueByDay[];
}

export class ImportService {
  private static BASE_URL = "/imports";
  public static async findFiltered(request: ImportFilterRequest) {
    return api.get<PaginatedResponse<ImportSearchResponse>>(
      this.BASE_URL + "/find-filtered",
      {
        params: {
          componentInfo: request.componentInfo,
          providerId: request.id,
          date: request.date,
          pageNumber: request.pageNumber,
          pageSize: request.pageSize,
        },
      }
    );
  }
  public static async findTotalInventoryValue() {
    return api.get<TotalInventoryValueRequest>(this.BASE_URL + "/total-value");
  }
  public static async findCountsThisWeek() {
    return api.get<ImportCountThisWeekResponse>(
      this.BASE_URL + "/find-counts-this-week"
    );
  }
}
