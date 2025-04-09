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
  public static async findCountsThisWeek() {
    return api.get<InvoiceCountThisWeekResponse>(
      this.BASE_URL + "/find-counts-this-week"
    );
  }
}
