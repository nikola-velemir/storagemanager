import api from "../infrastructure/Interceptor/Interceptor";
import { InvoiceSearchResponses } from "../model/invoice/InvoiceSearchResponses";

export interface InvoiceFilterRequest {
  componentInfo: string | null;
  id: string | null;
  date: string | null;
  pageNumber: number;
  pageSize: number;
}

export class InvoiceService {
  public static async findFiltered(request: InvoiceFilterRequest) {
    return api.get<InvoiceSearchResponses>("/invoices/find-filtered", {
      params: {
        componentInfo: request.componentInfo,
        providerId: request.id,
        date: request.date,
        pageNumber: request.pageNumber,
        pageSize: request.pageSize,
      },
    });
  }
}
