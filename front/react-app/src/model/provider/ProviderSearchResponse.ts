import { ProviderInvoiceResponse } from "./ProviderInvoiceResponse";

export interface ProviderSearchResponse {
  id: string;
  name: string;
  address: string;
  phoneNumber: string;
  invoices: ProviderInvoiceResponse[];
}
