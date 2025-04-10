import api from "../infrastructure/Interceptor/Interceptor";
import { InvoiceCountThisWeekResponse } from "../model/invoice/InvoiceFindCountsForWeekResponseDTO";
import { InvoiceSearchResponse } from "../model/invoice/InvoiceSearchResponse";
import { PaginatedResponse } from "../model/PaginatedResponse";

export interface InvoiceFilterRequest {
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

export interface TotalInvetoryValueRequest {
  total: number;
  values: InventoryValueByDay[];
}

export class InvoiceService {
  private static BASE_URL = "/invoices";
  public static async findFiltered(request: InvoiceFilterRequest) {
    return api.get<PaginatedResponse<InvoiceSearchResponse>>(
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
    return api.get<TotalInvetoryValueRequest>(this.BASE_URL + "/total-value");
  }
  public static async findCountsThisWeek() {
    return api.get<InvoiceCountThisWeekResponse>(
      this.BASE_URL + "/find-counts-this-week"
    );
  }
}
