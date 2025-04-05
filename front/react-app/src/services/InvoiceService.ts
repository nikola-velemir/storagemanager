import api from "../infrastructure/Interceptor/Interceptor";
import { InvoiceSearchResponses } from "../model/invoice/InvoiceSearchResponses";

export class InvoiceService {
  public static async findAll() {
    return api.get<InvoiceSearchResponses>("/invoices");
  }
}
