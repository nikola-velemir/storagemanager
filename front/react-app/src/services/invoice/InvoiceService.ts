import api from "../../infrastructure/Interceptor/Interceptor";
import { InvoiceFindResponse } from "../../model/invoice/InvoiceFindResponse";

interface InvoiceTypeResponse {
  type: string;
  isProcessed: boolean;
}

export class InvoiceService {
  private static BASE_URL = "/invoices";

  public static async findInvoiceType(id: string) {
    return api.get<InvoiceTypeResponse>(this.BASE_URL + "/" + id);
  }
  public static async findInvoiceByPartner(id: string) {
    return api.get<InvoiceFindResponse[]>(this.BASE_URL + "/partner/" + id);
  }
}
