import api from "../../infrastructure/Interceptor/Interceptor";

interface InvoiceTypeResponse {
  type: string;
}

export class InvoiceService {
  private static BASE_URL = "/invoices";

  public static async findInvoiceType(id: string) {
    return api.get<InvoiceTypeResponse>(this.BASE_URL + "/" + id);
  }
}
