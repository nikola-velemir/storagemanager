import api from "../infrastructure/Interceptor/Interceptor";
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
  public static async findFiltered(request: InvoiceFilterRequest) {
    return api.get<PaginatedResponse<InvoiceSearchResponse>>(
      "/invoices/find-filtered",
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
}
